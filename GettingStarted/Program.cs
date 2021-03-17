using MassTransit;
using System;
using System.Threading.Tasks;

namespace GettingStarted
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingInMemory(sbc => {

                sbc.ReceiveEndpoint("test_queue", ep => {

                    ep.Handler<Message>(context => {

                        return Console.Out.WriteLineAsync($"Received : {context.Message.Text}");
                    });
                });
            });


            await bus.StartAsync();

            await bus.Publish(new Message { Text = "Hi" });

            Console.WriteLine("Press any key to exit");

            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }
    }

    public class Message
    {
        public string Text { get; set; }
    }
}
    