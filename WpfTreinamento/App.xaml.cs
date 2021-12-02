using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfTreinamento.Contratos;
using WpfTreinamento.Repositorios;

namespace WpfTreinamento
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();
            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<FormTimes>();
            services.AddScoped<IConexao, ConexaoSQLSERVER>();
            services.AddScoped<IConexao, ConexaoMySQL>();
            services.AddScoped<IConexao, ConexaoPostgresSQL>();
            services.AddScoped<IViewModelIntermediate, ViewModelIntermediate>();
            await using ServiceProvider container = services.BuildServiceProvider();

            var mainWindow = container.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
