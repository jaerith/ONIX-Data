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

NOTE: Even though this project addresses many tags of both ONIX versions, it does not currently parse out all of them, especially in the case of ONIX 3.0 (which appears to aim at supporting the ontology of the known universe).  In the case that you find something unsupported and wanted, you can create an issue within this repo, and I will attempt to address it in my free time.  (Or you can implement it on your own and then issue a pull.)

# For Large ONIX Files
When parsing larger ONIX files (typically anything greater than 250 MB), it's strongly encouraged to use the OnixLegacyPlusParser class (for ONIX 2.1) and the OnixPlusParser class (for ONIX 3.0).  These two classes are used just like the OnixLegacyParser and OnixParser classes, and they will help the user to avoid out-of-memory exceptions.  However, there is one caveat to know before using either of them: the ONIX-Data project does perform preprocessing on the ONIX file before doing any actual parsing.  These changes are merely real-world substitutions for ONIX encodings (found in the ONIX DTD), which is the same result for the output when parsing with a DTD. However, in the case of large files, we are actually changing the file itself, and it can take a few minutes to finish (like 6-8 minutes per 400 MB), depending on the machine's specs.  So, if you value the original copy of your large ONIX file, be sure to create a backup copy beforehand.

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
