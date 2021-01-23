using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TezInceleme
{
	public static class Helper
	{
		public static bool TextIsSekil(string text)
		{
			if (text == null) return false;
			if (!text.StartsWith(Constants.SEKIL)) return false;

			if (text.Length < 10) return false;

			if (text[5] != ' ') return false;// Şekil 3.5.
			if (text[7] != '.') return false;//şekildeki aradaki noktanın kontrolü
			if (text[9] != '.') return false;//şekildeki aradaki noktanın kontrolü


			if (!IsNumeric(text.Substring(6, 1))) return false;//Şekilden sonra gelen karakterler numericmi kontrolu
			if (!IsNumeric(text.Substring(6, 1))) return false;


			return true;
		
		}
		public static bool TextIsTablo(string text)
		{
			if (text == null) return false;

			if (!text.StartsWith(Constants.TABLO)) return false;

			if (text.Length < 10) return false;

			if (text[5] != ' ') return false;
			if (text[7] != '.') return false;
			if (text[9] != '.') return false;


			if (!IsNumeric(text.Substring(6, 1))) return false;
			if (!IsNumeric(text.Substring(6, 1))) return false;


			return true;

		}

		
		public static bool IsNumeric(this string text) => double.TryParse(text, out _);//ggelen bir string değerin numericmi değiilmi diye kontrolü sağlanıyor.
	}
}
