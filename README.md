<!-- Will address quality gate and new code coverage in the future -->
<!-- [![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=jaerith_ONIX-Data)](https://sonarcloud.io/dashboard?id=jaerith_ONIX-Data) -->

[![Reliability](https://sonarcloud.io/api/project_badges/measure?project=jaerith_ONIX-Data&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=jaerith_ONIX-Data)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=jaerith_ONIX-Data&metric=security_rating)](https://sonarcloud.io/dashboard?id=jaerith_ONIX-Data)
[![Maintainability](https://sonarcloud.io/api/project_badges/measure?project=jaerith_ONIX-Data&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=jaerith_ONIX-Data)

[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=jaerith_ONIX-Data&metric=ncloc)](https://sonarcloud.io/dashboard?id=jaerith_ONIX-Data)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=jaerith_ONIX-Data&metric=bugs)](https://sonarcloud.io/dashboard?id=jaerith_ONIX-Data)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=jaerith_ONIX-Data&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=jaerith_ONIX-Data)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=jaerith_ONIX-Data&metric=coverage)](https://sonarcloud.io/dashboard?id=jaerith_ONIX-Data)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=jaerith_ONIX-Data&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=jaerith_ONIX-Data)

# ONIX-Data
This solution provides a C# library, for both Framework and Standard, that serves to provide .NET data structures (and an accompanying set of helpful parsers) for the ONIX XML format, which is the international standard for representing the electronic data regarding books (along with other media).  This format has been established by the international book trade body known as EDITEUR.  Within this solution, you will find two collections of classes for serialization/deserialization: one that represents the legacy format (i.e., 2.1 and earlier) and another that represents the current format (i.e., 3.0).  In addition, two helpful parser classes have been included in order to assist with the population of those collections.

Even though the "sunset date" for the legacy version 2.1 has passed, many (if not most) organizations still use 2.1 for the time being, and they will likely be used for the near future.

Unfortunately, since validation of ONIX files has proven problematic on the .NET platform, there is an <a target="_blank" href="https://github.com/jaerith/ONIX-Validator">accompanying Java project</a> that can serve to validate those files instead.

NOTE: The Framework project is now considered to be deprecated.  All future development will only occur in the Standard project.

# Requirements
* Visual Studio 2019 (at least)
* An unconditional love for a XML tag collection that attempts to cover the ontology of the known universe.

# ONIX Editions Handled
* [x] ONIX 3.0 (short tags)
* [x] ONIX 3.0 (reference tags)
* [x] ONIX 2.1.3 and earlier (short tags)
* [x] ONIX 2.1.3 and earlier (reference tags)

NOTE: Even though this project addresses many tags of both ONIX versions, it does not currently parse out all of them, especially in the case of ONIX 3.0 (which appears to aim at supporting the ontology of the known universe).  In the case that you find something unsupported and wanted, you can create an issue within this repo, and I will attempt to address it in my free time.  (Or you can implement it on your own and then issue a pull.)  The same applies for any possible features that can be incorporated into the Extensions folder (like autocorrection with ChatGPT, etc.).

# For Large ONIX Files
When parsing larger ONIX files (typically anything greater than 250 MB), it's strongly encouraged to use the OnixLegacyPlusParser class (for ONIX 2.1) and the OnixPlusParser class (for ONIX 3.0).  These two classes are used just like the OnixLegacyParser and OnixParser classes, and they will help the user to avoid out-of-memory exceptions.

# Notes

There is one caveat to know before using any of the Parsers: the ONIX-Data project does perform non-optional preprocessing on the ONIX file before doing any actual parsing.  These changes are merely real-world substitutions for ONIX encodings (found in the ONIX DTD), which is the same result for the output when parsing with a DTD.  These non-optional replacements actually change the file itself, and it can take a few minutes to finish (like 6-8 minutes per 400 MB), depending on the machine's specs.  So, if you value the original copy of your ONIX file (i.e., with non-standard ONIX encodings), be sure to create a backup copy beforehand.

The Parsers also have an optional preprocessing step (invoked via the constructor), which will perform other friendly edits (like removing misformed HTML encodings, etc.) that will clean the file of any suspicious characters.  These characters can cause the Microsoft XML libraries to throw an exception.

If you would like to become better acquainted with legacy format of the ONIX standard, you can find documentation and relevant files (XSDs, DTDs, etc.) on <a target="_blank" href="http://www.editeur.org/15/Archived-Previous-Releases/">the archive page of EDITEUR</a>.

If you would like to become better acquainted with the current version of the ONIX standard, you can find documentation and relevant files (XSDs, DTDs, etc.) on <a target="_blank" href="http://www.editeur.org/93/Release-3.0-Downloads/">the current page of EDITEUR</a>.

# Projects

Project Source | Nuget_Package |  Description |
------------- |--------------------------|-----------|
[OnixData](https://github.com/jaerith/ONIX-Data/tree/master/OnixData)    | https://www.nuget.org/packages/ONIX-Data/ | This C# library serves to provide .NET data structures (and an accompanying set of helpful parsers) for the ONIX XML format. |
[OnixData.Standard](https://github.com/jaerith/ONIX-Data/tree/master/OnixData.Standard) | https://www.nuget.org/packages/ONIX-Data.Standard/ | Packaged as a .NET Standard library, this C# library serves to provide .NET data structures (and an accompanying set of helpful parsers) for the ONIX XML format. |
[OnixData.Standard.Benchmarks](https://github.com/jaerith/ONIX-Data/tree/master/OnixData.Standard.Benchmarks)    | | This project benchmarks the Standard version of the library, running its own simple tests against a variety of sample sizes and providing reports of its performance.|
[OnixData.Standard.BaseTests](https://github.com/jaerith/ONIX-Data/tree/master/OnixData.Standard.BaseTests)| | This library contains more thorough unit tests of several ONIX sample files, which will then be employed in validating the library against various Microsoft frameworks.|
[OnixData.Standard.NetFrameworkTests](https://github.com/jaerith/ONIX-Data/tree/master/OnixData.Standard.NetFrameworkTests)| | This project uses the BaseTests project to run unit tests against the .NET 4.6 framework. |
[OnixData.Standard.CoreTests](https://github.com/jaerith/ONIX-Data/tree/master/OnixData.Standard.CoreTests)| | This project uses the BaseTests project to run unit tests against the .NET Core framework. |
[OnixData.Standard.Net5Tests](https://github.com/jaerith/ONIX-Data/tree/master/OnixData.Standard.Net5Tests)| | This project uses the BaseTests project to run unit tests against the .NET 5 framework. |
[OnixTestHarness](https://github.com/jaerith/ONIX-Data/tree/master/OnixTestHarness)| | This project is a simple test harness that provides some use cases on how to use the ONIX-Data parser. |

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
