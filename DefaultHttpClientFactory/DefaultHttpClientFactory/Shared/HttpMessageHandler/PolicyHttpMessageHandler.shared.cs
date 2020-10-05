using Plugin.DefaultHttpClientFactory.Shared.Extensions;
using Plugin.DefaultHttpClientFactory.Shared.i18N;
using Polly;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.DefaultHttpClientFactory.Shared.HttpPolly
{
    /// <summary>
    /// DISCLAIMER: It's an copy/paste(isolated mode, no dependencies) from original sorce code implementation of Microsoft.Extensions.Http.Polly nuget
    /// https://github.com/dotnet/extensions/tree/master/src/HttpClientFactory/Polly/src
    /// </summary>
    internal class PolicyHttpMessageHandler : DelegatingHandler
    {
        private readonly IAsyncPolicy<HttpResponseMessage> _policy;
        private readonly Func<HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> _policySelector;

        /// <summary>
        /// Creates a new <see cref="PolicyHttpMessageHandler"/>.
        /// </summary>
        /// <param name="policy">The policy.</param>
        public PolicyHttpMessageHandler(IAsyncPolicy<HttpResponseMessage> policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            _policy = policy;
        }

        /// <summary>
        /// Creates a new <see cref="PolicyHttpMessageHandler"/>.
        /// </summary>
        /// <param name="policySelector">A function which can select the desired policy for a given <see cref="HttpRequestMessage"/>.</param>
        public PolicyHttpMessageHandler(Func<HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> policySelector)
        {
            if (policySelector == null)
            {
                throw new ArgumentNullException(nameof(policySelector));
            }

            _policySelector = policySelector;
        }

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Guarantee the existence of a context for every policy execution, but only create a new one if needed. This
            // allows later handlers to flow state if desired.
            var cleanUpContext = false;
            var context = request.GetPolicyExecutionContext();
            if (context == null)
            {
                context = new Context();
                request.SetPolicyExecutionContext(context);
                cleanUpContext = true;
            }

            HttpResponseMessage response;
            try
            {
                var policy = _policy ?? SelectPolicy(request);
                response = await policy.ExecuteAsync((c, ct) => SendCoreAsync(request, c, ct), context, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (cleanUpContext)
                {
                    request.SetPolicyExecutionContext(null);
                }
            }

            return response;
        }

        /// <summary>
        /// Called inside the execution of the <see cref="Policy"/> to perform request processing.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/>.</param>
        /// <param name="context">The <see cref="Context"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>Returns a <see cref="Task{HttpResponseMessage}"/> that will yield a response when completed.</returns>
        protected virtual Task<HttpResponseMessage> SendCoreAsync(HttpRequestMessage request, Context context, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return base.SendAsync(request, cancellationToken);
        }

        private IAsyncPolicy<HttpResponseMessage> SelectPolicy(HttpRequestMessage request)
        {
            var policy = _policySelector(request);
            if (policy == null)
            {
                var message = string.Format(Resources.PolicyHttpMessageHandler_PolicySelector_ReturnedNull,
                    "policySelector",
                    "Policy.NoOpAsync<HttpResponseMessage>()");
                throw new InvalidOperationException(message);
            }

            return policy;
        }
    }
}
