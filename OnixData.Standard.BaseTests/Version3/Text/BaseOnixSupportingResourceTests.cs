using System.IO;
using System.Linq;
using OnixData.Standard.Version3;
using Xunit;

namespace OnixData.Standard.BaseTests.Version3.Text
{
    public abstract class BaseOnixSupportingResourceTests
    {
        [Fact]
        public void ParseOnix3SupportingResourceSample1()
        {
            var sFilepath = "Samples/Onix3SupportingResourceSample1.xml";

            var currentFileInfo = new FileInfo(sFilepath);
            using OnixPlusParser v3Parser = new(currentFileInfo, true);

            foreach (OnixProduct tmpProduct in v3Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);
                
                Assert.Equal(1, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().ResourceContentType);
                Assert.Equal(0, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().ContentAudienceList.First());
                
                Assert.Equal(2, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().ResourceForm);
                
                Assert.Equal(1, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().OnixResourceVersionFeatureList.First().ResourceVersionFeatureType);
                Assert.Equal("D504", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().OnixResourceVersionFeatureList.First().FeatureValue);
                
                Assert.Equal("http://www.publisher.com/covers/ 9780001234567.tif", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().ResourceLinksList.First());
                Assert.Equal(17, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().ContentDatesList.First().ContentDateRole);
                
                Assert.Equal(1, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().ResourceForm);
                
                Assert.Equal(1, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.First().ResourceVersionFeatureType);
                Assert.Equal("D502", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.First().FeatureValue);
                Assert.Equal(2, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.Skip(1).First().ResourceVersionFeatureType);
                Assert.Equal("171", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.Skip(1).First().FeatureValue);
                Assert.Equal(3, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.Skip(2).First().ResourceVersionFeatureType);
                Assert.Equal("125", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.Skip(2).First().FeatureValue);
                
                Assert.Equal("http://www.publisher.com/covers/ 9780001234567.jpg", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().ResourceLinksList.First());
                Assert.Equal(17, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().ContentDatesList.First().ContentDateRole);
            }
        }
        
        [Fact]
        protected void ParseOnix3SupportingResourceSample1ShortTags()
        {
            var sFilepath = "Samples/Onix3SupportingResourceSample1ShortTags.xml";

            var currentFileInfo = new FileInfo(sFilepath);
            using OnixParser v3Parser = new(currentFileInfo, true);

            foreach (OnixProduct tmpProduct in v3Parser)
            {
                Assert.True(tmpProduct.IsValid(), tmpProduct.GetParsingError()?.Message);
                
                Assert.Equal(1, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().ResourceContentType);
                Assert.Equal(0, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().ContentAudienceList.First());
                
                Assert.Equal(2, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().ResourceForm);
                
                Assert.Equal(1, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().OnixResourceVersionFeatureList.First().ResourceVersionFeatureType);
                Assert.Equal("D504", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().OnixResourceVersionFeatureList.First().FeatureValue);
                
                Assert.Equal("http://www.publisher.com/covers/ 9780001234567.tif", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().ResourceLinksList.First());
                Assert.Equal(17, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(0).First().ContentDatesList.First().ContentDateRole);
                
                Assert.Equal(1, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().ResourceForm);
                
                Assert.Equal(1, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.First().ResourceVersionFeatureType);
                Assert.Equal("D502", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.First().FeatureValue);
                Assert.Equal(2, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.Skip(1).First().ResourceVersionFeatureType);
                Assert.Equal("171", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.Skip(1).First().FeatureValue);
                Assert.Equal(3, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.Skip(2).First().ResourceVersionFeatureType);
                Assert.Equal("125", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().OnixResourceVersionFeatureList.Skip(2).First().FeatureValue);
                
                Assert.Equal("http://www.publisher.com/covers/ 9780001234567.jpg", tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().ResourceLinksList.First());
                Assert.Equal(17, tmpProduct.CollateralDetail.OnixSupportingResourceList.First().OnixResourceVersionList.Skip(1).First().ContentDatesList.First().ContentDateRole);
            }
        }
    }
}