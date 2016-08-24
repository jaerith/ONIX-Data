# ONIX-Data
This solution is a C# library that serves to provide .NET data structures (and an accompanying set of helpful parsers) for the ONIX XML format, which is the international standard for representing the electronic data regarding books (along with other media).  This format has been established by the international book trade body known as EDITEUR.  Within this solution, you will find two collections of classes for serialization/deserialization: one that represents the legacy format (i.e., 2.1 and earlier) and another that represents the current format (i.e., 3.0).  In addition, two helpful parser classes have been included in order to assist with the population of those collections.

Even though the "sunset date" for the legacy version 2.1 has passed, many (if not most) organizations still use 2.1 for the time being, and they will likely be used for the near future.

Unfortunately, since validation of ONIX files has proven problematic on the .NET platform, there is an <a target="_blank" href="https://github.com/jaerith/ONIX-Validator">accompanying Java project</a> that can serve to validate those files instead.

# NOTES

If you would like to become better acquainted with legacy format of the ONIX standard, you can find documentation and relevant files (XSDs, DTDs, etc.) on <a target="_blank" href="http://www.editeur.org/15/Archived-Previous-Releases/">the archive page of EDITEUR</a>.

If you would like to become better acquainted with the current version of the ONIX standard, you can find documentation and relevant files (XSDs, DTDs, etc.) on <a target="_blank" href="http://www.editeur.org/93/Release-3.0-Downloads/">the current page of EDITEUR</a>.
