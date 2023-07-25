using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.IO;



HashSet<string> final = new HashSet<string>();                  //The HashSet that will be printed when the program is done


///<summary>
///Gets all links and subpages from the string url and returns them in the form of a list of strings
///<param name="url">the url of the page</param>
///</summary>
List<string> getUrls(string url)                                
{                                                               
    List<string> res = new List<string>();
    try
    {
        HtmlWeb page = new HtmlWeb();
        HtmlDocument doc = page.Load(url);
        var baseURL = new Uri(url);
        HtmlNodeCollection col = doc.DocumentNode.SelectNodes("//a[@href]");
        if (col != null)
        {
            foreach (HtmlNode link in col)
            {
                HtmlAttribute attr = link.Attributes["href"];
                var url1 = new Uri(baseURL, attr.Value);
                res.Add(url1.ToString());
            }
        }
    }
    catch { }
        return res;
}

///<summary>
///Adds all subpages within a page to the queue　if they are not already in the queue
///<param name="q">queue that the pages will be extracted to</param>
///<param name="url">the url of the page</param>
///</summary>
void LinkQueue(Queue<string> q, string url)                     //Adds all subpages within a page to the queue
{                                                               //if they are not already in the queue

        List<string> done = getUrls(url);
    if(done != null)
        if (done.Count > 0)
            foreach (string item in done)
                    if (!q.Contains(item))  
                    q.Enqueue(item);
    
            
}

///<summary>
///Prints all links in a webpage, as well as subpages up to a certain level
///<param name="homepage">the starting webpage</param>
///<param name="level">how deep the function will go</param>
///</summary>
void solve(string homepage,int level)
{


    Queue<string> links = new Queue<string>();              //Queue for the links to be added to the final HashSet

    LinkQueue(links, homepage);                             //Enqueue the URLs in the first level to the queue

    string x;
    for (int i = 1; i <= level; i++)                        //Loop for every level
    {
        int queueCount = links.Count;                       //Gets the count of the URL queue -- which would be at any given time equal to the number of URLs in this level
        for (int c = 0; c < queueCount; c++)                //Go over every URL in the current level and add them to final if they are not already present
        { 
            x = links.Dequeue();

                if(final.Add(x))
                 LinkQueue(links, x);
            
        }
    }
    links.Clear();                                          //Deletes the queue to save space

    StreamWriter writer = new StreamWriter("output/links.txt");       //Writer to write the output in a text file
    foreach (string url in final)
        writer.WriteLine(url);
    Console.WriteLine("The text file has been successfully created! Please check the output subfolder within the program's folder.");
    writer.Close();
}

//Main Program below!

string home;
int inp;
Uri send;
Console.Write("Enter desired Level: ");
    while (!int.TryParse(Console.ReadLine(), out inp) || inp <= 0)              //Loops if the level is invalid or less than 1
{
    Console.WriteLine("You must enter a valid integer greater than zero.");
}
Console.Write("Enter desired website: ");
home = Console.ReadLine();
while (!Uri.IsWellFormedUriString(home, UriKind.Absolute))                      //Checks if the input is a valid website or not. If not, repeat the input
{
    Console.WriteLine("Please enter a valid website.");
    home = Console.ReadLine();
}
solve(home, Convert.ToInt32(inp));                                              //Function call
