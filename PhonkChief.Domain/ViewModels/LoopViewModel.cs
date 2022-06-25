using PhonkChief.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhonkChief.Domain.ViewModels
{
    public class LoopViewModel
    {
        [Required(ErrorMessage = "Загрузите файл формата wav")]
        public byte[] File { get; set; }
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Выберите категорию")]
        public int Category { get; set; }
        [Required(ErrorMessage = "Укажите параметр")]
        public int Bpm { get; set; }
    }
}
