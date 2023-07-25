# UrlExtractor
A program that extracts all URLs from a website, including its subpages until a certain level of depth. The ".exe" file is located inside the "bin/debug/net6.0" folder. The output is in a text file "links.txt" inside a directory "output" within the same directory as the ".exe" file.

## Input Format
The program will ask for an input "level", which only accepts integers greater than zero. A "level" indicates how deep the recursive function will go through subpages. Level 1 will print all the links within the input website, while higher levels will iterate through those subpages as well.

The program will also ask the user for a string which would be the starting webpage.

**The program requires internet connection to function.**
