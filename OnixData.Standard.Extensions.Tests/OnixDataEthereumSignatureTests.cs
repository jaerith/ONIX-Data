using OnixData.Standard.Extensions.Models;

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
		<MessageNote><![CDATA[The Product list of this message was signed with the private key of did:ethr:0x94618601FE6cb8912b274E5a00453949A57f8C1e, resulting in the signature (0x9ef18f99f80f1940748b353334b58d13a345e18eb1af5dc5de304802e4980a5318e86cad394ae8a690b787a3bfc77541462c524b96194a16ffd836e447832f981b).]]></MessageNote>
	</Header>	
	<!-- product record 1 of 1 in message -->
	<Product>
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
				<PublisherName>My Mom's Basement</PublisherName>
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

			string sMessageNote =
				onixContent.GenerateSignedMessageNote(publicAddress, privateKey);

			using (OnixParser parser = new OnixParser(onixContent.XmlText, false, false, true))
			{
				Assert.True(parser.Message.DetectMsgNoteEthereumSignature());

				Assert.Equal(sMessageNote, parser.MessageHeader.MessageNote);

                bool isEthereumSignatureValid = 
					parser.Message.ValidateMsgNoteEthereumSignature(onixContent);

				Assert.True(isEthereumSignatureValid);
			}
		}
	}
}