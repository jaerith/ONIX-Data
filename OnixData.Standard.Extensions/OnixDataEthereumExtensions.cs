using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using Nethereum.Signer;
using Nethereum.Web3;

using OnixData.Version3;
using OnixData.Version3.Publishing;

using OnixData.Standard.Extensions.Models;
using OnixData.Version3.Header;

namespace OnixData.Standard.Extensions
{
    public static class OnixDataEthereumExtensions
    {
        public const string OnixIdTypeNameDID = @"W3C CCG DID";

        public const string Onix3BasicMessageFormat = 
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<ONIXMessage release=""3.0"">
    {0}
	<Product>
		<DescriptiveDetail>
			<ProductForm>{1}</ProductForm>
            {2}
            {3}
			<Language>
				<LanguageRole>01</LanguageRole>
				<LanguageCode>{4}</LanguageCode>
			</Language>
			<Subject>
				<MainSubject/>
				<SubjectSchemeIdentifier>10</SubjectSchemeIdentifier>
				<SubjectSchemeVersion>2017</SubjectSchemeVersion>
				<SubjectCode>{5}</SubjectCode>
			</Subject>
		</DescriptiveDetail>
		<PublishingDetail>
			<Publisher>
				<PublishingRole>01</PublishingRole>
				<PublisherName>{6}</PublisherName>
			</Publisher>
			<PublishingDate>
				<PublishingDateRole>01</PublishingDateRole>
				<Date dateformat=""00"">{7}</Date>
			</PublishingDate>
		</PublishingDetail>
	</Product>
</ONIXMessage>
         ";

        public const string Onix3HeaderFormat =
@"	<Header>
		<Sender>
            {0}
			{1}
		</Sender>
		<SentDateTime>{2}</SentDateTime>
		<MessageNote><![CDATA[{3}]]></MessageNote>
	</Header>";

        public const string Onix3SenderIdFormat =
@"			<SenderIdentifier>
				<SenderIDType>{0}</SenderIDType>
			    <IDTypeName>{1}</IDTypeName>
				<IDValue>{2}</IDValue>
			</SenderIdentifier>";

        public const string Onix3SenderNameFormat =
@"            <SenderName>{0}</SenderName>";

		public const string Onix3TitleFormat =
@"			<TitleDetail>
				<TitleType>01</TitleType>
				<TitleElement>
					<SequenceNumber>1</SequenceNumber>
					<TitleElementLevel>01</TitleElementLevel>
					<NoPrefix/>
					<TitleWithoutPrefix textcase=""01""><![CDATA[{0}]]></TitleWithoutPrefix>
				</TitleElement>
			</TitleDetail>
";

        public const string Onix3BasicCntbFormat =
@"			<Contributor>
				<ContributorRole>A01</ContributorRole>
                {0}
                {1}
			</Contributor>";

        public const string Onix3BasicCntbIdFormat =
@"				<NameIdentifier>
					<NameIDType>{0}</NameIDType>
					<IDTypeName>{1}</IDTypeName>
					<IDValue>{2}</IDValue>
				</NameIdentifier>";

        public const string Onix3BasicCntbNameFormat =
@"				<NamesBeforeKey>{0}</NamesBeforeKey>
				<KeyNames>{1}</KeyNames>";

		public const string Onix3BasicCntbPersonNameFormat =
@"                <PersonName>{0}</PersonName>";

        public const string DidEthPrefix = "did:ethr:";

        public const string SignedProductListMessageNoteFormatStart =
@"The Product list of this message was signed with the private key of did:ethr:";

        public const string SignedProductListMessageNoteFormat =
SignedProductListMessageNoteFormatStart + @"{0}, resulting in the signature ({1}).";

        public const string StartProductRefTag   = "<Product";
        public const string StartProductShortTag = "<product";

        public const string EndProductRefTag   = "</Product>";
        public const string EndProductShortTag = "</product>";

		public static string CleanXml(this string xmlContent)
        {
			return xmlContent.Replace("\r\n", "\n").Replace("\t", "  ");
		}

        public static bool DetectMsgNoteEthereumSignature(this OnixMessage onixMessage)
        {
            bool containsEthereumSignature = false;

            if ((onixMessage.Header != null) && !String.IsNullOrEmpty(onixMessage.Header.MessageNote))
            {
                containsEthereumSignature =
                    onixMessage.Header.MessageNote.StartsWith(SignedProductListMessageNoteFormatStart);
            }

            return containsEthereumSignature;
        }

        public static string GenerateMessageNote(this string onixContent, string publicKey, string signature)
        {
            string messageNote = String.Empty;

            if (!String.IsNullOrEmpty(onixContent))
            {
                messageNote =
                    String.Format(SignedProductListMessageNoteFormat, publicKey, signature);
            }

            return messageNote;
        }

        public static string GenerateSignedMessageNote(this OnixXmlText onixContent, string publicKey, string privateKey)
        {
            string messageNote = String.Empty;

            if (!String.IsNullOrEmpty(onixContent.XmlText))
            {
                string productList = onixContent.GetProductList();

                var signer = new EthereumMessageSigner();

                var signature = signer.EncodeUTF8AndSign(productList, new EthECKey(privateKey));

                messageNote =
                    String.Format(SignedProductListMessageNoteFormat, publicKey, signature);
            }

            return messageNote;
        }

        public static string GetProductList(this OnixXmlText onixContent)
        {
            string productList = String.Empty;

            if (!String.IsNullOrEmpty(onixContent.XmlText))
            {
                var startTag = String.Empty;
                var endTag   = String.Empty;

                if (onixContent.XmlText.Contains(StartProductRefTag) && onixContent.XmlText.Contains(EndProductRefTag))
                {
                    startTag = StartProductRefTag;
                    endTag   = EndProductRefTag;
                }
                else if (onixContent.XmlText.Contains(StartProductShortTag) && onixContent.XmlText.Contains(EndProductShortTag))
                {
                    startTag = StartProductShortTag;
                    endTag   = EndProductShortTag;
                }

                if (!String.IsNullOrEmpty(startTag))
                {
                    var productListIdx = onixContent.XmlText.IndexOf(startTag);
                    var productListLen = (onixContent.XmlText.LastIndexOf(endTag) - productListIdx) + endTag.Length;

                    productList = onixContent.XmlText.Substring(productListIdx, productListLen);
                }

            }

            return productList;
        }

		public static string PrepareFinalOnixMessage(this string onixContent)
		{
            var firstNewlineIdx = -1;

            if ((firstNewlineIdx = onixContent.IndexOf("\n")) > 0)
            {
				var secondNewlineIdx = onixContent.IndexOf("\n", firstNewlineIdx+1);
				if (secondNewlineIdx > 0)
				{
                    onixContent = onixContent.Remove(secondNewlineIdx, 1);
                }
            }

            return onixContent;
        }

        public static string PrettyPrintXml(this OnixXmlText onixContent, bool removeFirstNewLine = true)
        {
			var prettyPrintBuilder = new StringBuilder();

			var element = XElement.Parse(onixContent.XmlText);

			var settings = new XmlWriterSettings();
			settings.OmitXmlDeclaration  = false;
			settings.Indent              = true;
			settings.NewLineOnAttributes = true;

			using (var xmlWriter = XmlWriter.Create(prettyPrintBuilder, settings))
			{
				element.Save(xmlWriter);
			}

            string newOnixXmlContent = prettyPrintBuilder.ToString().CleanXml();

            return removeFirstNewLine ? newOnixXmlContent.PrepareFinalOnixMessage() : newOnixXmlContent;
		}

        public static string ToSimpleOnixString(this OnixProduct onixProduct, string headerMsgNote = null)
        {
            var header      = String.Empty;
            var contribList = String.Empty;

            if (onixProduct.PrimaryAuthor != null)
            {
                var cntbIds   = String.Empty;
                var cntbNames = String.Empty;

                var senderIds   = String.Empty;
                var senderNames = String.Empty;
				
                if (onixProduct.PrimaryAuthor.OnixNameIdList.Any())
                {
                    var firstNameId = onixProduct.PrimaryAuthor.OnixNameIdList[0];

                    cntbIds = String.Format(Onix3BasicCntbIdFormat
                                            , firstNameId.NameIDType
                                            , firstNameId.IDTypeName
                                            , firstNameId.IDValue);

                    senderIds = String.Format(Onix3SenderIdFormat
                                            , firstNameId.NameIDType
                                            , firstNameId.IDTypeName
                                            , firstNameId.IDValue);
                }

                if (!String.IsNullOrEmpty(onixProduct.PrimaryAuthor.OnixKeyNames))
                {
                    cntbNames = String.Format(Onix3BasicCntbNameFormat
                                              , onixProduct.PrimaryAuthor.OnixNamesBeforeKey
                                              , onixProduct.PrimaryAuthor.OnixKeyNames);

                    senderNames = String.Format(Onix3SenderNameFormat
                                                , onixProduct.PrimaryAuthor.OnixKeyNames);
                }

                header = String.Format(Onix3HeaderFormat
                                       , senderIds
                                       , senderNames
                                       , DateTime.Now.ToString("YYYYMMDD")
                                       , headerMsgNote ?? String.Empty);

                contribList = String.Format(Onix3BasicCntbFormat, cntbIds, cntbNames);
            }

            return String.Format(Onix3BasicMessageFormat
                                 , header
                                 , onixProduct.ProductForm
                                 , onixProduct.Title
                                 , contribList
                                 , onixProduct.DescriptiveDetail?.LanguageOfText ?? String.Empty
                                 , onixProduct.DescriptiveDetail?.OnixMainSubjectList[0].MainSubject ?? String.Empty
                                 , onixProduct.PublisherName
                                 , onixProduct.PublishingDetail?.PublicationDate
                                );

        }

        public static bool ValidatePublisherEnsName(this OnixPublisher onixPublisher, string web3Url)
        {
            bool validEnsName = false;

            var web3       = new Web3(web3Url);
		    var ensService = web3.Eth.GetEnsService();

            if (!String.IsNullOrEmpty(onixPublisher.PublisherName) && onixPublisher.PublisherName.ToLower().Trim().EndsWith(".eth"))
            {
                var publicAddress = ensService.ResolveAddressAsync(onixPublisher.PublisherName).Result;

                validEnsName = !String.IsNullOrEmpty(publicAddress);
            }

            return validEnsName;
        }

        public static bool ValidateSenderEnsName(this OnixHeaderSender onixSender, string web3Url)
        {
            bool validEnsName = false;

            var web3       = new Web3(web3Url);
            var ensService = web3.Eth.GetEnsService();

            if (onixSender.OnixSenderIdentifierList != null)
            {
                foreach (var onixSenderId in onixSender.OnixSenderIdentifierList)
                {
                    if (!String.IsNullOrEmpty(onixSenderId.IDValue) && 
                        onixSenderId.IDValue.ToLower().Trim().EndsWith(".eth"))
                    {
                        var publicAddress = ensService.ResolveAddressAsync(onixSenderId.IDValue).Result;

                        validEnsName = !String.IsNullOrEmpty(publicAddress);
                        break;
                    }
                }
            }

            return validEnsName;
        }

        public static bool ValidateMsgNoteEthereumSignature(this OnixMessage onixMessage, OnixXmlText rawOnixContent)
        {
            bool validEthereumSignature = false;

            if (onixMessage.DetectMsgNoteEthereumSignature())
            {
                var messageNote = onixMessage.Header.MessageNote;
                var didEthIndex = messageNote.IndexOf(DidEthPrefix);
                
                if (didEthIndex > 0)
                {
                    var sigEthStartIndex = messageNote.IndexOf("(", didEthIndex);
                    var sigEthEndIndex   = messageNote.IndexOf(")", sigEthStartIndex);

                    if (sigEthEndIndex > sigEthStartIndex)
                    {
                        var signer = new EthereumMessageSigner();

                        var ethPublicAddrStartIdx = didEthIndex + DidEthPrefix.Length;

                        var ethereumPublicAddress =
                            messageNote.Substring(ethPublicAddrStartIdx, 
                                                  (messageNote.IndexOf(",", didEthIndex) - ethPublicAddrStartIdx)).Trim();

                        var ethereumSignature =
                            messageNote.Substring(sigEthStartIndex + 1, (sigEthEndIndex - sigEthStartIndex - 1)).Trim();

                        string productList = rawOnixContent.GetProductList();

                        // Validate the address
                        var addressRecovered = signer.EncodeUTF8AndEcRecover(productList, ethereumSignature);

                        validEthereumSignature = (ethereumPublicAddress == addressRecovered);
                    }
                }
            }

            return validEthereumSignature;
        }
    }
}
