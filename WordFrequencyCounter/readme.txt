Original problem description:

Create a frequency dictionary for a given text file.

Details:
The program should be implemented without using ChatGPT or any other assistant.
The program should be implemented as a console application. The first parameter is the name of the input file. The second parameter specifies the name of the file with the result.
The encoding of the source and result files is considered to be Windows-1252.
Words are separated by spaces or newline characters. Words are compared case-insensitive.
The format of the resulting file – "{0},{1}\n" where {0} is a word, and {1} is the number of its occurrences in the source file.
The resulting file should be sorted in descending order of occurrence.
The application should have the following qualities:

1) Performance, reasonable economy of CPU and Memory resources.
2) Scalability on multicore/multiprocessor systems.
3) Expect incorrect input from the user.
4) Have a modular decomposition and contain unit tests.
5) Have a convenient architecture for further expansion of functionality.
6) Have a description of the architecture in any convenient form.

Notes:

Please see the launchSettings.json to set your working directory as required.
Note that the two parameters are specified as "input.txt" and "output.txt", I've populatd "input.txt" with some example data, I have not catered for non-words or extra whitespace but could do this if spec required.
Please delete the "output.txt" after running as the validation will not allow program to continue if the second parameter for output file already exists (as a precaution).
I've added some comments within the code to explain some decisions that I made.
In response to points in the original problem description:

1) Performance, reasonable economy of CPU and Memory resources.
	
	I've used IAsyncEnumerable to attempt to keep memory footprint lower when reading from file.
	I've used yield return when getting data to output to file to again not create all data at once.
	I've added cancellation token support to allow processing to finish more quickly if cancellation has been requested.
	I've used a concurrent dictionary within a singleton such that each thread can access and update the same shared collection.
	
2) Scalability on multicore/multiprocessor systems.
	
	I've used ParallelForEachAsync to allow parallel processing of lines.
	I've not adjusted the default parallelism specification but this could be done if required.

3) Expect incorrect input from the user.
	
	I've added validation to protect against incorrect user input, it has not been done to be fully exhaustive. Exception handling has been added, but not extensively as no recovery will be considered.

4) Have a modular decomposition and contain unit tests.

	I've split out the solution into different folders and then split the logic into different classes as appropriate.
	I've added test cases using NUnit - I've primarily used TestCaseSource, I know I can also use automatic data generation where appropriate.
	I did consider adding benchmarking tests but have ommitted them as their use would require much larger data file to be processed etc to be worthwhile (I could create on the fly potentially)
	I am aware that I could also supply random data, potentially use AutoFixuture etc for some tests 

5) Have a convenient architecture for further expansion of functionality.

	I've separated different areas of concern into different interfaces. Different classes then implement them and could be swapped out via DI / derivation where relevant.
	I've used Autofac for DI / IoC.

6) Have a description of the architecture in any convenient form.
	
	I would say the architecture could be stated to be along the lines of map reduce, but in particular it uses divide and conquer approach, breaking the file into individual lines (pieces of work) and then finally putting the results together.
