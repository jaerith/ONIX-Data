using System;

using OpenAI.Chat;

namespace OnixData.Services
{
    public class OnixTextRepairChatService
    {
        public const int CONST_LARGE_REQUEST_SIZE_THRESHOLD = 25000;

        public const string RETURN_ERROR_MSG_PREFIX = @"ERROR!  ";

        public const string CONST_REQUEST_TAG_FIX_MESSAGE_FORMAT =
@"Repair the following XML:\n{0}";

        public const string CONST_REQUEST_HTML_FIX_MESSAGE_FORMAT =
@"Repair the following HTML so that it is valid and all tags are paired, without any reordering or omitting text and without converting the text into a HTML document that starts with <!DOCTYPE html> and without adding more text than necessary:\n{0}";

        public const string CONST_GPT4_RESPONSE_PADDED_START = "\n";
        public const string CONST_GPT4_RESPONSE_HTML_START = "```html";
        public const string CONST_GPT4_RESPONSE_HTML_END = "```";

        private OnixChatServiceSettings _settings;

        private ChatClient _defaultOpenAIChatClient;
        private ChatClient _largeRequestOpenAIChatClient;

        public OnixTextRepairChatService(OnixChatServiceSettings settings)
        {
            _settings = settings;

            var keyCreds = new System.ClientModel.ApiKeyCredential(settings.ApiKey);

            var defaultOptions =
                new OpenAI.OpenAIClientOptions() { NetworkTimeout = new TimeSpan(0, 0, settings.TimeoutInSeconds) };

            _defaultOpenAIChatClient =
                new ChatClient(settings.Model, keyCreds, defaultOptions);

            var largeRequestOptions =
                new OpenAI.OpenAIClientOptions() { NetworkTimeout = new TimeSpan(0, 10, 0) };

            _largeRequestOpenAIChatClient =
                new ChatClient(settings.Model, keyCreds, largeRequestOptions);
        }

        public string RetrieveRepairedCommHtml(string commTextTag, ref TimeSpan requestLength)
        {
            string repairedCommTextTag = commTextTag;

            string promptFormat = CONST_REQUEST_HTML_FIX_MESSAGE_FORMAT;
            if (!String.IsNullOrEmpty(_settings.Prompt))
                promptFormat = _settings.Prompt;

            string promptRequest = String.Format(promptFormat, commTextTag);

            DateTime requestStartTime = DateTime.Now;

            ChatCompletion completion =
                commTextTag.Length < CONST_LARGE_REQUEST_SIZE_THRESHOLD ?
                _defaultOpenAIChatClient.CompleteChat(promptRequest) :
                _largeRequestOpenAIChatClient.CompleteChat(promptRequest);

            if ((completion != null) && (completion.Content.Count > 0))
            {
                repairedCommTextTag = completion.Content[0].Text;
                if (!String.IsNullOrEmpty(repairedCommTextTag) && repairedCommTextTag.Contains(CONST_GPT4_RESPONSE_HTML_START))
                {
                    int htmlStart =
                        repairedCommTextTag.IndexOf(CONST_GPT4_RESPONSE_HTML_START) + CONST_GPT4_RESPONSE_HTML_START.Length;

                    int htmlEnd = repairedCommTextTag.IndexOf(CONST_GPT4_RESPONSE_HTML_END, htmlStart);
                    if (htmlEnd > htmlStart)
                    {
                        repairedCommTextTag =
                            repairedCommTextTag.Substring(htmlStart, htmlEnd - htmlStart).Trim();
                    }
                }
            }

            requestLength = DateTime.Now.Subtract(requestStartTime);

            if (repairedCommTextTag.StartsWith(CONST_GPT4_RESPONSE_PADDED_START))
                repairedCommTextTag = repairedCommTextTag.Remove(0, CONST_GPT4_RESPONSE_PADDED_START.Length);

            return repairedCommTextTag;
        }
    }
}
