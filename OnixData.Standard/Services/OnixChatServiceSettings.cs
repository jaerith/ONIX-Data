namespace OnixData.Standard.Services
{
    public class OnixChatServiceSettings
    {
        public string ApiKey { get; set; }
        public string Model { get; set; }
        public string Prompt { get; set; }
        public string Suffix { get; set; }
        public decimal Temperature { get; set; }
        public int MaxTokens { get; set; }
        public decimal TopP { get; set; }
        public string Stop { get; set; }

        public int TimeoutInSeconds { get; set; }

        public OnixChatServiceSettings()
        {
            Model = "gpt-4o-mini";
            Suffix = null;
            Temperature = 0.3M;
            MaxTokens = 2000;
            TopP = 1.0m;
            Stop = "[END]";
            TimeoutInSeconds = 180;
        }
    }
}
