﻿namespace BlogCore.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }

        public IEnumerable<Article> listArticles { get; set; }
    }
}
