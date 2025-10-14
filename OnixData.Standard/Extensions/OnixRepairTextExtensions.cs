using System;

using OnixData.Standard.Legacy;
using OnixData.Standard.Services;
using OnixData.Standard.Version3.Text;

namespace OnixData.Standard.Extensions
{
    public static class OnixRepairTextExtensions
    {
        /// <summary>
        /// 
        /// Repairs any malformed HTML in the Text field of an OnixLegacyOtherText object
        /// 
        /// </summary>
        public static TimeSpan RepairHtmlText(this OnixLegacyOtherText legacyOtherText, OnixChatServiceSettings chatServiceSettings)
        {
            TimeSpan timeSpan = new TimeSpan(0);

            if (!String.IsNullOrEmpty(legacyOtherText.Text))
            {
                OnixTextRepairChatService repairService = new OnixTextRepairChatService(chatServiceSettings);

                legacyOtherText.Text = repairService.RetrieveRepairedCommHtml(legacyOtherText.Text, ref timeSpan);
            }

            return timeSpan;
        }

        /// <summary>
        /// 
        /// Repairs any malformed HTML in the Text field of an OnixTextContent object
        /// 
        /// </summary>
        public static TimeSpan RepairHtmlText(this OnixTextContent onixTextContent, OnixChatServiceSettings chatServiceSettings)
        {
            TimeSpan timeSpan = new TimeSpan(0);

            if (!String.IsNullOrEmpty(onixTextContent.Text))
            {
                OnixTextRepairChatService repairService = new OnixTextRepairChatService(chatServiceSettings);

                onixTextContent.Text = repairService.RetrieveRepairedCommHtml(onixTextContent.Text, ref timeSpan);
            }

            return timeSpan;
        }
    }
}