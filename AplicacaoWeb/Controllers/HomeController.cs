using AplicacaoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AplicacaoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static List<TaskItem> listaTarefas = new List<TaskItem>();
        private static int proximoId = 1;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            if(task.Id == 0)
            {
                task.Id = proximoId++;
                listaTarefas.Add(task);
            }
            else
            {
                TaskItem existingTask = listaTarefas.FirstOrDefault(t => t.Id == task.Id);

                if (existingTask != null)
                {
 
                    existingTask.Titulo = task.Titulo;
                    existingTask.Descricao = task.Descricao;
                    existingTask.Pontuacao = task.Pontuacao;
                }
                
            }

            return RedirectToAction("Listar");
        }

        public IActionResult Listar()
        {
            return View(listaTarefas);
        }

        public IActionResult Excluir(int id)
        {
            var tarefa = listaTarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa != null)
            {
                listaTarefas.Remove(tarefa);
            }
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult ObterTarefa(int id)
        {
            var tarefa = listaTarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa != null)
            {
                return View("Cadastrar", tarefa);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult PesquisarTarefa(string termo)
        {

            var tarefasEncontradas = listaTarefas.Where(t => t.Titulo.Contains(termo) || t.Descricao.Contains(termo)).ToList();

            return View("Listar",tarefasEncontradas);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}