// See https://aka.ms/new-console-template for more information
using Anagrams;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Main: bootstrapping");

var services = new ServiceCollection();
services.AddScoped<WordRepository>();
services.AddScoped<AnagramService>();

var provider = services.BuildServiceProvider().CreateScope().ServiceProvider;

var anagramsService = provider.GetRequiredService<AnagramService>();

Console.WriteLine("Please, enter a word...");
var input = Console.ReadLine();
List<string> anagrams;
Task<List<string>> task;

// SYNCHRONOUS

Console.WriteLine("------------------ SYNCHRONOUS EXECUTION ------------------");

Console.WriteLine("This is what we found");
anagrams = anagramsService.GetAnagrams(input);

foreach (var word in anagrams)
{
    Console.WriteLine(word);
}

// NAIVE ASYNCHRONOUS

Console.WriteLine("------------------ NAIVE ASYNCHRONOUS EXECUTION ------------------");

task = anagramsService.GetAnagramsNaiveAsync(input);
Console.WriteLine("This is what we found");

anagrams = await task;

foreach (var word in anagrams)
{
    Console.WriteLine(word);
}

// ASYNCHRONOUS

Console.WriteLine("------------------ TRUE ASYNCHRONOUS EXECUTION ------------------");

task = anagramsService.GetAnagramsAsync(input);
var task2 = anagramsService.GetAnagramsAsync(input);

Console.WriteLine("This is what we found");

await Task.WhenAll();

anagrams = await task;

foreach (var word in anagrams)
{
    Console.WriteLine(word);
}

