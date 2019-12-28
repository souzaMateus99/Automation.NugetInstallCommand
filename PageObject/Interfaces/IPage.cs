using System;
using ScrapySharp.Network;


namespace PageObject.Interfaces
{
    public interface IPage
    {
        IPage Submit();

        string ExtractContentOfPage();
    }
}