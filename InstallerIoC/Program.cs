// See https://aka.ms/new-console-template for more information

using InstallerIoCModule;
using Microsoft.Extensions.DependencyInjection;
var services = new ServiceCollection();
services.AddModule();
Console.WriteLine("Hello, World!");
