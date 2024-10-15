The application was written quickly, with the intention of solving an issue I had with converting version 2.1 .vcf (vCard) file to version 3.0. The latter is the preffered type of contact file for NextCloud (current version as I write this of 30.0.1).

I looked for applications or websites to achieve what I wanted, but discovered they were either too complex, or the 'free' trial version limited you to only 5 contacts. 

I was just looking for a really simple solution to my issue, so as I couldn't find one, I thought 'why not just write one?', so grabbed myself an IDE for that purpose and went to it. Initially I was going to write all the parsing classes myself, but came across the library vCardLib, and rather than re-invent the wheel, realised I should just use that instead. 

The application itself doesn't do much apart from convert the input file into the output file, however, it does check for extra spaces at the end of the input file and strips them, as this is a common cause of the inability to import .vcf files (apparently!).

Use at your own risk, but apart from being silly and opening a file with viral content or something with it, it should do exactly what it says. There's no backwards conversion... i.e. you can't go from 3.0 to 2.1 but given its limitations, it does what it's designed for. The source code is available for you if you want to do more with it, but it's quick and dirty, so don't expect anything flash. It's written in C#.

In fact, I've just noticed that I called it VFCConvert, whereas it really should VCFConvert! As I said, quick and dirty!

Wez Hind 15/10/2024
