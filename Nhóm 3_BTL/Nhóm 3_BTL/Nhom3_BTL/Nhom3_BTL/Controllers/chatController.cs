
using ChatGPT;
using ChatGPT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace Nhom3_BTL_NguyenDuyHung.Controllers
{
    
    public class chatController : Controller
    {
        private static readonly string apiKey = "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";
        public static string model = "gpt-3.5-turbo";

        // To store the chat history for the current session
        private static string chatHistory = "";

        [Route("Chat")]
        public ActionResult Index()
        {
            chatHistory = "";  // Reset chat history on new session
            return View("~/Views/Chat/Index.cshtml");
        }

        [HttpPost]
        [Route("Chat/SendMessage")]
        public async Task<ActionResult> SendMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return Json(new { success = false, message = "Message cannot be empty." });

            // Append the user message to the history
            chatHistory += $"User: {message}\n";

            // Call OpenAI API to get a response
            string botResponse = await GetOpenAIResponse(message);

            // Append the bot response to the history
            chatHistory += $"Bot: {botResponse}\n";

            // Return the response to the client
            return Json(new { success = true, message = botResponse });
        }

        [HttpPost]
        [Route("Chat/CreateImage")]
        public async Task<ActionResult> CreateImage(string message, string count, string size)
        {
            if (string.IsNullOrWhiteSpace(message))
                return Json(new { success = false, message = "Message cannot be empty." });

            // Call OpenAI API to generate an image based on the description
            string imageUrl = await GenerateImage(message, count, size);

            return Json(new { success = true, imageUrl });
        }

        // Method to interact with OpenAI GPT model
        private async Task<string> GetOpenAIResponse(string message)
        {
            using (var client = new HttpClient())
            {
                var requestBody = new
                {
                    model = "gpt-3.5-turbo", // Hoặc model bạn đang sử dụng
                    messages = new[]
                    {
                new { role = "user", content = message }
            }
                };

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                    string botMessage = jsonResponse.choices[0].message.content;
                    return botMessage;
                }
               
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Lỗi: " + errorResponse);
                    return $"Lỗi: {response.StatusCode} - {errorResponse}";
                }
            }
        }

        // Method to interact with OpenAI's DALL-E model for image generation
        private async Task<string> GenerateImage(string prompt, string count, string size)
        {
            using (var client = new HttpClient())
            {
                var requestBody = new
                {
                    prompt = prompt,
                    n = int.Parse(count), // Number of images
                    size = size // Image size (e.g., "1024x1024")
                };

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
                var response = await client.PostAsync("https://api.openai.com/v1/images/generations", new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                    string imageUrl = jsonResponse.data[0].url; // Extract the first image URL
                    return imageUrl;
                }

                return "Error generating image.";
            }
        }
    }
}