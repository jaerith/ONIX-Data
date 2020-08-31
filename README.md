# ONIX-Data
This solution is a C# library that serves to provide .NET data structures (and an accompanying set of helpful parsers) for the ONIX XML format, which is the international standard for representing the electronic data regarding books (along with other media).  This format has been established by the international book trade body known as EDITEUR.  Within this solution, you will find two collections of classes for serialization/deserialization: one that represents the legacy format (i.e., 2.1 and earlier) and another that represents the current format (i.e., 3.0).  In addition, two helpful parser classes have been included in order to assist with the population of those collections.

Even though the "sunset date" for the legacy version 2.1 has passed, many (if not most) organizations still use 2.1 for the time being, and they will likely be used for the near future.

Unfortunately, since validation of ONIX files has proven problematic on the .NET platform, there is an <a target="_blank" href="https://github.com/jaerith/ONIX-Validator">accompanying Java project</a> that can serve to validate those files instead.

# Requirements
* Visual Studio 2012 (at least)
* An unconditional love for a XML tag collection that attempts to cover the ontology of the known universe.

# ONIX Editions Handled
* [x] ONIX 3.0 (short tags)
* [x] ONIX 3.0 (reference tags)
* [x] ONIX 2.1.3 and earlier (short tags)
* [x] ONIX 2.1.3 and earlier (reference tags)

# Notes

If you would like to become better acquainted with legacy format of the ONIX standard, you can find documentation and relevant files (XSDs, DTDs, etc.) on <a target="_blank" href="http://www.editeur.org/15/Archived-Previous-Releases/">the archive page of EDITEUR</a>.

If you would like to become better acquainted with the current version of the ONIX standard, you can find documentation and relevant files (XSDs, DTDs, etc.) on <a target="_blank" href="http://www.editeur.org/93/Release-3.0-Downloads/">the current page of EDITEUR</a>.

# Usage Examples

    // An example of using the ONIX parser for the contemporary ONIX standard (i.e., 3.0)
    int nOnixPrdIdx = 0;
    string sFilepath = @"YourVer3OnixFilepath.xml";

    FileInfo CurrentFileInfo = new FileInfo(sFilepath);
    using (OnixParser V3Parser = new OnixParser(CurrentFileInfo, true))
    {
        OnixHeader Header = V3Parser.MessageHeader;

        foreach (OnixProduct TmpProduct in V3Parser)
        {
            string tmpISBN = TmpProduct.ISBN;

            var Title       = TmpProduct.Title;
            var Author      = TmpProduct.PrimaryAuthor;
            var Language    = TmpProduct.DescriptiveDetail.LanguageOfText;
            var PubDate     = TmpProduct.PublishingDetail.PublicationDate;
            var SeriesTitle = TmpProduct.SeriesTitle;
            var USDPrice    = TmpProduct.USDRetailPrice;

            var BarCodes = TmpProduct.OnixBarcodeList;

            /*
             * The IsValid method will inform the caller if the XML within the Product tag is invalid due to syntax
             * or due to invalid data types within the tags (i.e., a Price with text).
             *
             * (The functionality to fully validate the product in accordance with the ONIX standard is beyond the scope
             * of this library.)
             *
             * If the product is valid, we can use it; if not, we can record its issue.  In this way, we can proceed 
             * with parsing the file, without being blocked by a problem with one record.
             */
            if (TmpProduct.IsValid())
            {
                System.Console.WriteLine("Product [" + (nOnixPrdIdx++) + "] has EAN(" +
                                         TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                         ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                                         
                /*
                * For 1-to-many composites, where a product can have more than one subitem (like Contributor), you should
                * use the lists that have a prefix of 'Onix', so that you can avoid having to detect whether or not the
                * reference or short composites have been used.
                */
                if (TmpProduct.DescriptiveDetail.OnixContributorList != null)
                {
                    foreach (var TmpContrib in TmpProduct.DescriptiveDetail.OnixContributorList)
                    {
                        System.Console.WriteLine("\tAnd has a contributor with key name (" + TmpContrib.KeyNames + ").");
                    }
                }                                         
            }
            else
            {
                System.Console.WriteLine(TmpProduct.GetParsingError());
            }
        }
    }

    // An example of using the ONIX parser for the legacy ONIX standard (i.e., 2.1)
    int nLegacyShortIdx = 0;
    string sLegacyShortFilepath = @"YourOnixFilepath.xml";
    using (OnixLegacyParser onixLegacyShortParser = new OnixLegacyParser(new FileInfo(sLegacyShortFilepath), true))
    {
        OnixLegacyHeader Header = onixLegacyShortParser.MessageHeader;

        // Check some values of the header
    
        foreach (OnixLegacyProduct TmpProduct in onixLegacyShortParser)
        {
            string Ean = TmpProduct.EAN;

            /*
             * The IsValid method will inform the caller if the XML within the Product tag is invalid due to syntax
             * or due to invalid data types within the tags (i.e., a Price with text).
             *
             * (The functionality to fully validate the product in accordance with the ONIX standard is beyond the scope
             * of this library.)
             *
             * If the product is valid, we can use it; if not, we can record its issue.  In this way, we can proceed 
             * with parsing the file, without being blocked by a problem with one record.
             */
            if (TmpProduct.IsValid())
            {
                System.Console.WriteLine("Product [" + (nLegacyShortIdx++) + "] has EAN(" +
                                         TmpProduct.EAN + ") and USD Retail Price(" + TmpProduct.USDRetailPrice.PriceAmount +
                                         ") - HasUSRights(" + TmpProduct.HasUSRights() + ").");
                                         

                /*
                 * For 1-to-many composites, where a product can have more than one subitem (like Contributor), you should
                 * use the lists that have a prefix of 'Onix', so that you can avoid having to detect whether or not the
                 * reference or short composites have been used.
                 */
                if (TmpProduct.OnixContributorList != null)
                {
                    foreach (OnixLegacyContributor TempContrib in TmpProduct.OnixContributorList)
                    {
                        System.Console.WriteLine("\tAnd has a contributor with key name (" + TempContrib.KeyNames + ")."); 
                    }
                }
            }

            }
            else
            {
                System.Console.WriteLine(TmpProduct.GetParsingError());
            }
        }
    }
