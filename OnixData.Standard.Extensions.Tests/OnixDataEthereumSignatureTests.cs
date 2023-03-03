using OnixData.Version3;

using OnixData.Standard.Extensions.Models;
using OnixData.Version3.Publishing;

namespace OnixData.Standard.Extensions.Tests
{
	public class OnixDataEthereumSignatureTests
	{
		public const string SAMPLE_V3_ONIX_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<ONIXMessage release=""3.0"">
	<Header>
		<Sender>
			<SenderIdentifier>
				<SenderIDType>01</SenderIDType>
			    <IDTypeName>W3C CCG DID</IDTypeName>
				<IDValue>did:ethr:0x94618601FE6cb8912b274E5a00453949A57f8C1e</IDValue>
			</SenderIdentifier>
			<SenderName>Anonymous</SenderName>
		</Sender>
		<MessageNumber>1</MessageNumber>
		<SentDateTime>20230110T1115-0400</SentDateTime>
		<MessageNote><![CDATA[The Product list of this message was signed with the private key of did:ethr:0x94618601FE6cb8912b274E5a00453949A57f8C1e, resulting in the signature (0xb4adb206870e19ae608e243de5331ea76ab6eba4b6f22ae6845c99721d42d18163ae68a5a963b180f31478b741a1944aaf93df1fb7d422b381ea9c2516c7d7761c).]]></MessageNote>
	</Header>	
	<!-- product record 1 of 1 in message -->
	<Product>
        <RecordReference>secretsofflukeman.johnsonbooks.nick.eth</RecordReference>
		<DescriptiveDetail>
			<ProductComposition>00</ProductComposition>
			<ProductForm>BC</ProductForm>
			<ProductFormDetail>B105</ProductFormDetail>
			<TitleDetail>
				<TitleType>01</TitleType>
				<TitleElement>
					<SequenceNumber>1</SequenceNumber>
					<TitleElementLevel>01</TitleElementLevel>
					<TitleWithoutPrefix textcase=""01"">Secrets of Flukeman</TitleWithoutPrefix>
				</TitleElement>
			</TitleDetail>
			<Contributor>
				<SequenceNumber>1</SequenceNumber>
				<ContributorRole>A01</ContributorRole>
				<NameIdentifier>
					<NameIDType>01</NameIDType>
					<IDTypeName>W3C CCG DID</IDTypeName>
					<IDValue>did:ethr:0x94618601FE6cb8912b274E5a00453949A57f8C1e</IDValue>
				</NameIdentifier>
			</Contributor>
			<Contributor>
				<SequenceNumber>2</SequenceNumber>
				<ContributorRole>A01</ContributorRole>
				<NamesBeforeKey>The</NamesBeforeKey>
				<KeyNames>Flukeman</KeyNames>
			</Contributor>
			<Language>
				<LanguageRole>01</LanguageRole>
				<LanguageCode>eng</LanguageCode>
			</Language>
			<Subject>
				<MainSubject/>
				<SubjectSchemeIdentifier>10</SubjectSchemeIdentifier>
				<SubjectSchemeVersion>2017</SubjectSchemeVersion>
				<SubjectCode>FIC022000</SubjectCode>
			</Subject>
			<Audience>
				<AudienceCodeType>01</AudienceCodeType>
				<AudienceCodeValue>01</AudienceCodeValue>
			</Audience>
		</DescriptiveDetail>
		<PublishingDetail>
			<Publisher>
				<PublishingRole>01</PublishingRole>
                <PublisherName>nick.eth</PublisherName>
            </Publisher>
			<PublishingDate>
				<PublishingDateRole>01</PublishingDateRole>
				<Date dateformat=""00"">20240807</Date>
			</PublishingDate>
		</PublishingDetail>
	</Product>
</ONIXMessage>
";

		[Fact]
		public void ValidateEthereumSignatureTest()
		{
			var publicAddress = "0x94618601FE6cb8912b274E5a00453949A57f8C1e";
			var privateKey    = "0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0";
			var onixContent   = new OnixXmlText(SAMPLE_V3_ONIX_XML);
            string web3Url    = String.Empty;

			// NOTE: To test certain extension methods, supply your own Web3 Url

			string sMessageNote =
				onixContent.GenerateSignedMessageNote(publicAddress, privateKey);

			using (OnixParser parser = new OnixParser(onixContent.XmlText, false, false, true))
			{
				Assert.True(parser.Message.DetectMsgNoteEthereumSignature());

				Assert.Equal(sMessageNote, parser.MessageHeader.MessageNote);

                bool isEthereumSignatureValid = 
					parser.Message.ValidateMsgNoteEthereumSignature(onixContent);

				Assert.True(isEthereumSignatureValid);

				if (!String.IsNullOrEmpty(web3Url))
				{
                    foreach (OnixProduct onixProduct in parser)
                    {
                        if ((onixProduct.PublishingDetail != null) &&
                            (onixProduct.PublishingDetail.OnixPublisherList.Count() > 0))
                        {
                            onixProduct.PublishingDetail
                                       .OnixPublisherList
                                       .Where(x => x.PublisherName.ToLower().Contains(".eth"))
                                       .ToList()
                                       .ForEach(x => Assert.True(x.ValidatePublisherEnsName(web3Url)));
                        }
                    }
                }
            }
		}
	}
}