using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the battleship game.");

            using IHost host = CreateHostBuilder(args).Build();

            RunProgram(host.Services);
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IYesNoInput, YesNoInput>()
                            .AddTransient<ICoordinateInput, CoordinateInput>()
                            .AddTransient<ConsoleWrapper>()
                            .AddTransient<IGameBoardInitializer, GameBoardInitializer>()
                            .AddTransient<IBattleshipGameRunner, BattleshipGameRunner>()
                            .AddTransient<GameLoop>());

        static void RunProgram(IServiceProvider services)
        {
            var gameLoop = services.GetRequiredService<GameLoop>();
            gameLoop.Run();
        }
    }
}
