using Microsoft.Office.Interop.Word;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using Word = Microsoft.Office.Interop.Word;
namespace TezInceleme
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void bn_openThesis_Click(object sender, EventArgs e)
		{
			OpenFileDialog file = new OpenFileDialog();
			file.Filter = "World Dosyası |*.doc| World Dosyası|*.docx";//Word dosyası seçileceği için uzantı .doc yada docx olarak ayarlanmıştır.
			file.FilterIndex = 2;
			if (file.ShowDialog() == DialogResult.OK)//Dosya seçimi tamam ise;
			{
				txt_thesispath.Text = file.FileName;// formdaki texte seçilen dosya atanır.
				lblErrorMessage.Visible = false;
			}
		}

		private void thesisProcess_DoWork(object sender, DoWorkEventArgs e)//Thread start edildiğinde bu fonksiyona girer.
		{

			int failCount = 0;

			thesisProcess.ReportProgress(1);//Yüzdeyi göstermek için kullandığımız method ReportProgres(x) x sinyalini gönderir.
			
			Microsoft.Office.Interop.Word.Application app = new Word.Application();
			Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(txt_thesispath.Text);//Yukarda seçilen dosyayı Document objesinin içine yükleme işlemi.
			Word.WdStatistic stat = Word.WdStatistic.wdStatisticPages;
			object missing = System.Reflection.Missing.Value;
			int pageCountNumber = doc.ComputeStatistics(stat, ref missing);//Sayfa sayısını num değişkenine atıyoruz.

			thesisProcess.ReportProgress(3);//Yüzde 3

			//İlk sayfa sayısını kontrol ediyoruz.
			if (pageCountNumber < 40 || pageCountNumber > 180)
			{
				failCount++;
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("İdeal tez uzunluğu 40 - 180 sayfa arasında olmalıdır! Mevcut sayfa sayısı:"+pageCountNumber);

				}));

			}
			
			float leftmargin = doc.PageSetup.LeftMargin;//Sol boşluk,
			float rightmargin = doc.PageSetup.RightMargin;//Sağ boşluk,
			float topMargin = doc.PageSetup.TopMargin;//Üst boşluk,
			float bottomMargin = doc.PageSetup.BottomMargin;//Alt boşluk değerleri dokümandan okunuyor.

			if (rightmargin != 70.9f)//Sağ boşluğun kontrolü.
			{
				failCount++;

				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("Sağ kenar boşluğu 2.5 cm olmalıdır!");

				}));
			}

			if (bottomMargin != 70.9f)//Alt boşluğun kontrolü.
			{
				failCount++;

				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("Alt kenar boşluğu 2.5 cm olmalıdır!");

				}));
			}

			if (leftmargin != 92.15f)//Sol boşluğun kontrolü.
			{
				failCount++;

				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("Sol kenar boşluğu 3.25 cm olmalıdır!");

				}));
			}

			if (topMargin != 85.05f)//Üst boşluğun kontrolü.
			{
				failCount++;

				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("Üst kenar boşluğu 3.0 cm olmalıdır!");

				}));
			}
			thesisProcess.ReportProgress(5);

			int paragraphcount = doc.Paragraphs.Count;

			int counter = 0;
			int timesnewromancount = 0;
			int elevenpuntocounter = 0;

			bool onsozexists = false;
			bool icindekilerexist = false;
			bool ozetexists = false;
			bool kaynakExists = false;
			bool beyanexists = false;
			

			bool abstractExists = false;
			bool sekilListesiexists = false;
			bool eklerlistesiexists = false;
			bool simgelerveKisaltmalarExists = false;

			int ShapesCount = doc.InlineShapes.Count;
			int inShapesCount = doc.Shapes.Count;

			int tablocount = 0;

			int sekilcount = 0;

			Hashtable htSekil = new Hashtable();
			Hashtable htTablo = new Hashtable();

			double sekilCounter = 0;
			double tabloCounter = 0;

			foreach (Paragraph objParagraph in doc.Paragraphs)//Paragrafların her biri tek tek okunup objParagraph objesinin içine atılıyor.
			{
				if (objParagraph.Range.Font.Name == "Times New Roman")//Eğer font Times New Roman ise;
					timesnewromancount++;//Times New Roman sayısı bir arttırılıyor.

				if (objParagraph.Range.Font.Size == 11)//Font size'ı 11 ise;
					elevenpuntocounter++;//Font size'ı kontrol eden değişkenimiz bir arttırılıyor.
				if (objParagraph.Range.Text.Trim() == Constants.ICINDEKILER)//İçindekiler texti paragrafta bulundu mu?
				{
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.ICINDEKILER);
					object startplusone = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.ICINDEKILER)+1;

					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.ICINDEKILER) + Constants.ICINDEKILER.Length;


					Word.Range rangeFirstChar = doc.Range(ref start, ref startplusone);
					Word.Range rangeothers = doc.Range(ref startplusone, ref end);

					float textsizefirst = rangeFirstChar.Font.Size;
					float textsizeothers = rangeothers.Font.Size;

					if (textsizefirst != 16)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("İlk harf 16 punto olmalı! Bölüm :'İÇİNDEKİLER'");//Hata ekrana bastırılır..

						}));

					}


					if (textsizeothers != 13)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Başlığın ilk harfinden sonra gelen harfler 13 punto olmalı! Bölüm :'İÇİNDEKİLER'");//Hata ekrana bastırılır..

						}));

					}

					icindekilerexist = true;
				}
				if (objParagraph.Range.Text.Trim()== Constants.OZET)//Özet texti 
				{

					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.OZET);
					object startplusone = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.OZET) + 1;

					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.OZET) + Constants.OZET.Length;


					Word.Range rangeFirstChar = doc.Range(ref start, ref startplusone);
					Word.Range rangeothers = doc.Range(ref startplusone, ref end);

					float textsizefirst = rangeFirstChar.Font.Size;
					float textsizeothers = rangeothers.Font.Size;

					if (textsizefirst != 16)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("İlk harf 16 punto olmalı! Bölüm :'Özet'");//Hata ekrana bastırılır..

						}));

					}


					if (textsizeothers != 13)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Başlığın ilk harfinden sonra gelen harfler 13 punto olmalı! Bölüm :'Özet'");//Hata ekrana bastırılır..

						}));

					}
			

					ozetexists = true;
				}
				if (objParagraph.Range.Text.Trim()==Constants.ONSOZ)
				{
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.ONSOZ);
					object startplusone = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.ONSOZ) + 1;

					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.ONSOZ) + Constants.ONSOZ.Length;


					Word.Range rangeFirstChar = doc.Range(ref start, ref startplusone);
					Word.Range rangeothers = doc.Range(ref startplusone, ref end);

					float textsizefirst = rangeFirstChar.Font.Size;
					float textsizeothers = rangeothers.Font.Size;

					if (textsizefirst != 16)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("İlk harf 16 punto olmalı! Bölüm :'ÖNSÖZ'");//Hata ekrana bastırılır..

						}));

					}


					if (textsizeothers != 13)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Başlığın ilk harfinden sonra gelen harfler 13 punto olmalı! Bölüm :'ÖNSÖZ'");//Hata ekrana bastırılır..

						}));

					}
					onsozexists = true;
				}
				if (objParagraph.Range.Text.Trim()==Constants.KAYNAKLAR)
				{
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.KAYNAKLAR);
					object startplusone = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.KAYNAKLAR) + 1;

					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.KAYNAKLAR) + Constants.KAYNAKLAR.Length;


					Word.Range rangeFirstChar = doc.Range(ref start, ref startplusone);
					Word.Range rangeothers = doc.Range(ref startplusone, ref end);

					float textsizefirst = rangeFirstChar.Font.Size;
					float textsizeothers = rangeothers.Font.Size;

					if (textsizefirst != 16)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("İlk harf 16 punto olmalı! Bölüm :'KAYNAKLAR'");//Hata ekrana bastırılır..

						}));

					}


					if (textsizeothers != 13)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Başlığın ilk harfinden sonra gelen harfler 13 punto olmalı! Bölüm :'KAYNAKLAR'");//Hata ekrana bastırılır..

						}));

					}
					kaynakExists = true;
				}
				if (objParagraph.Range.Text.Trim() == Constants.ABSTRACT)
				{
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.ABSTRACT);
					object startplusone = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.ABSTRACT) + 1;

					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.ABSTRACT) + Constants.ABSTRACT.Length;


					Word.Range rangeFirstChar = doc.Range(ref start, ref startplusone);
					Word.Range rangeothers = doc.Range(ref startplusone, ref end);

					float textsizefirst = rangeFirstChar.Font.Size;
					float textsizeothers = rangeothers.Font.Size;

					if (textsizefirst != 16)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("İlk harf 16 punto olmalı! Bölüm :'ABSTRACT'");//Hata ekrana bastırılır..

						}));

					}


					if (textsizeothers != 13)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Başlığın ilk harfinden sonra gelen harfler 13 punto olmalı! Bölüm :'ABSTRACT'");//Hata ekrana bastırılır..

						}));

					}
					abstractExists = true;
				}

				if (objParagraph.Range.Text.Trim() == Constants.BEYAN)
				{
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.BEYAN);
					object startplusone = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.BEYAN) + 1;

					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.BEYAN) + Constants.BEYAN.Length;


					Word.Range rangeFirstChar = doc.Range(ref start, ref startplusone);
					Word.Range rangeothers = doc.Range(ref startplusone, ref end);

					float textsizefirst = rangeFirstChar.Font.Size;
					float textsizeothers = rangeothers.Font.Size;
					if (textsizefirst != 16)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("İlk harf 16 punto olmalı! Bölüm :'BEYAN'");//Hata ekrana bastırılır..

						}));

					}


					if (textsizeothers != 13)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Başlığın ilk harfinden sonra gelen harfler 13 punto olmalı! Bölüm :'BEYAN'");//Hata ekrana bastırılır..

						}));

					}
					beyanexists = true;
				}
				if (objParagraph.Range.Text.Trim()== Constants.SEKILLERLISTESI)
				{
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.SEKILLERLISTESI);
					object startplusone = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.SEKILLERLISTESI) + 1;

					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.SEKILLERLISTESI) + Constants.SEKILLERLISTESI.Length;


					Word.Range rangeFirstChar = doc.Range(ref start, ref startplusone);
					Word.Range rangeothers = doc.Range(ref startplusone, ref end);

					float textsizefirst = rangeFirstChar.Font.Size;
					float textsizeothers = rangeothers.Font.Size;
					if (textsizefirst != 16)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("İlk harf 16 punto olmalı! Bölüm :'ŞEKİLLER LİSTESİ'");//Hata ekrana bastırılır..

						}));

					}


					if (textsizeothers != 13)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Başlığın ilk harfinden sonra gelen harfler 13 punto olmalı! Bölüm :'ŞEKİLLER LİSTESİ'");//Hata ekrana bastırılır..

						}));

					}
					sekilListesiexists = true;
				}

				if (objParagraph.Range.Text.Trim() == Constants.EKLERLISTESI)
				{
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.EKLERLISTESI);
					object startplusone = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.EKLERLISTESI) + 1;

					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.EKLERLISTESI) + Constants.EKLERLISTESI.Length;


					Word.Range rangeFirstChar = doc.Range(ref start, ref startplusone);
					Word.Range rangeothers = doc.Range(ref startplusone, ref end);

					float textsizefirst = rangeFirstChar.Font.Size;
					float textsizeothers = rangeothers.Font.Size;

					if (textsizefirst != 16)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("İlk harf 16 punto olmalı! Bölüm :'EKLER LİSTESİ'");//Hata ekrana bastırılır..

						}));

					}


					if (textsizeothers != 13)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Başlığın ilk harfinden sonra gelen harfler 13 punto olmalı! Bölüm :'EKLER LİSTESİ'");//Hata ekrana bastırılır..

						}));

					}
					eklerlistesiexists = true;
				}

				if (objParagraph.Range.Text.Trim()== Constants.SIMGELERVEKISALTMALAR)
				{
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.SIMGELERVEKISALTMALAR);
					object startplusone = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.SIMGELERVEKISALTMALAR) + 1;

					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.SIMGELERVEKISALTMALAR) + Constants.SIMGELERVEKISALTMALAR.Length;


					Word.Range rangeFirstChar = doc.Range(ref start, ref startplusone);
					Word.Range rangeothers = doc.Range(ref startplusone, ref end);

					float textsizefirst = rangeFirstChar.Font.Size;
					float textsizeothers = rangeothers.Font.Size;

					if (textsizefirst != 16)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("İlk harf 16 punto olmalı! Bölüm :'SİMGELER VE KISALTMALAR'");//Hata ekrana bastırılır..

						}));

					}


					if (textsizeothers != 13)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Başlığın ilk harfinden sonra gelen harfler 13 punto olmalı! Bölüm :'SİMGELER VE KISALTMALAR'");//Hata ekrana bastırılır..

						}));

					}
					simgelerveKisaltmalarExists = true;
				}

				if (objParagraph.Range.Text.Contains(Constants.SEKIL))
				{
					//Şekil 2.3.
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.SEKIL);
					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.SEKIL) + Constants.SEKIL.Length + 5;
					Word.Range rangesekil = doc.Range(ref start, ref end);

					bool isSekild = Helper.TextIsSekil(rangesekil.Text);

					if (isSekild && !htSekil.ContainsKey(rangesekil.Text))//Eğer hashtable'da şekil yoksa kontrolü yapılıyor.
					{
						sekilcount++;//Yoksa şekil sayacı bir arttırılıyor.
						htSekil[rangesekil] = rangesekil.Text;//Hashtable'ın içine şekil atılıyor.

						double sekilNumber = Double.Parse( rangesekil.Text.Substring(6,3));// 4.3

						if (sekilNumber < sekilCounter)//sekilCounter en son şeklin numarasını tutar. Eğer yeni gelen şekil son şekilden küçükse hata vardır. 
						{
							failCount++;
							this.Invoke(new MethodInvoker(() =>
							{
								this.listResults.Items.Add("Şekil numaraları sırayla olmalıdır!");//Hata ekrana bastırılır..

							}));
						}
						else sekilCounter = sekilNumber;
					}

					if (isSekild && rangesekil.Font.Size < 10)//Eğer şekil gelmiş ve font size'ı 10'dan küçükse ekrana hata bastırılır.
					{
						failCount++;

						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Şekil kısımları en az 10 boyutunda olmalı!");

						}));

					}

				}
				//BEYAN
				if (objParagraph.Range.Text.Contains(Constants.TABLO))//Şekil için yapılan yukardaki bloğun tamamı tablo için de yapılıyor.
				{
					object start = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.TABLO);
					object end = objParagraph.Range.Start + objParagraph.Range.Text.IndexOf(Constants.TABLO) + Constants.TABLO.Length + 5;
					Word.Range rangetablo = doc.Range(ref start, ref end);

					bool istablo = Helper.TextIsTablo(rangetablo.Text);
					if (istablo && !htTablo.ContainsKey(rangetablo.Text))
					{
						tablocount++;
						htTablo[rangetablo] = rangetablo.Text;
					}

					if (istablo && rangetablo.Font.Size < 10)
					{
						failCount++;
						this.Invoke(new MethodInvoker(() =>
						{
							this.listResults.Items.Add("Tablo kısımları en az 10 boyutunda olmalı!");

						}));

					}

				}

				thesisProcess.ReportProgress(5 + (95*counter/doc.Paragraphs.Count));// Progress hesaplama yüzde hesaplama Toplam paragraf sayısının mevcut paragraf indexine oranını buluyoruz. Bunu yüzde olarak gösteriyoruz.

				counter++;
			}


			if ((ShapesCount + inShapesCount) < (tablocount + sekilcount))//Eğer dokümandaki shape countu tablo ve şekil count'ından küçükse o zaman tüm tablo ve şekillere isim verilmemiştir anlamına gelir.
			{//Bu durumda ekrana hata bastırılır.
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("Tüm şekil ve tabloların altına Şekil ve Tablo numarası yazılmalıdır!");

				}));

			}

			if (!beyanexists)//Yukarıdaki gibi
			{
				failCount++;
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("BEYAN bölümü eksik!");

				}));
			}
			if (!onsozexists)//Önsöz değişkenine yukarıda atama yapıldı. Eğer atamalar sonucunda değişken true'ya çevrilmemişse;
			{//bu kod işler. 
				failCount++;//fail sayısı bir arttırılır.

				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("ÖNSÖZ bölümü eksik!");// Önsöz bölümü eksik hatası ekrana bastırılır.

				}));
			}
			if (!icindekilerexist)//Eğer içindekiler değişkenine true atanmadıysa;
			{//hata ekrana bastırılır.
				failCount++;
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("İÇİNDEKİLER bölümü eksik!");

				}));
			}
			if (!ozetexists)//Yukardaki gibi
			{
				failCount++;
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("ÖZET bölümü eksik!");

				}));
			}
			if (!abstractExists)//Yukardaki gibi
			{
				failCount++;
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("ABSTRACT bölümü eksik!");

				}));
			}
		

			if (!sekilListesiexists)//Yukardaki gibi
			{
				failCount++;
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("ŞEKİLLER LİSTESİ bölümü eksik!");

				}));
			}
			if (!eklerlistesiexists)//Yukardaki gibi
			{
				failCount++;
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("EKLER LİSTESİ bölümü eksik!");

				}));
			}
			

			
			if (!simgelerveKisaltmalarExists)//Yukardaki gibi
			{
				failCount++;
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("SIMGELER VE KISALTMALAR bölümü eksik!");

				}));
			}
			if (!kaynakExists)//Yukardaki gibi
			{
				failCount++;
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("KAYNAKLAR bölümü eksik!");

				}));
			}
			if (timesnewromancount * 2 < paragraphcount)//Eğer tezin yarısından çoğu Times New Roman ile yazılmamışsa ekrana hata bastırılır.
			{
				failCount++;

				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("Tezin yazım fontu Times New Roman olmalıdır! Tezdeki Times New Roman oranı = %" + 100 * timesnewromancount / paragraphcount);

				}));
			}

			if (elevenpuntocounter * 1.5 < paragraphcount)//Tez içeriğindeki yazıların boyutu 11 punto olmalıdır. 
			{
				failCount++;

				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("Tezin font büyüklüğü 11 punto olmalıdır! Tezde bulunan 11 punto oranı= %" + 100 * elevenpuntocounter / paragraphcount);

				}));
			}
			

			if (failCount == 0)//Eğer hata sayısı 0 ise tezinizde hata bulunamadı diye mesaj gösteriyoruz.
			{

				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add("Tezinizde hiçbir hata bulunamadı!");

				}));

			}
			else
			{
				this.Invoke(new MethodInvoker(() =>
				{
					this.listResults.Items.Add(string.Format("Tezinizde bulunan hata başlığı sayısı:{0}",failCount));//Hata sayısı 0'dan farklıysa da tezde bulunan toplam hata sayısı gösteriliyor.

				}));

			}

			doc.Close();
			doc = null;
			//return num;
		}

		private void bn_start_Click(object sender, EventArgs e)
		{
			bn_openThesis.Enabled = false;
			bn_start.Enabled = false;//Başla butonu disabled ediliyor.
			if (thesisProcess.IsBusy)
			{
				MessageBox.Show("İşlem sürüyor...");
				return;
			}
			listResults.Items.Clear();//Eğer daha önceden tez incelendiyse sonuç listesi temizleniyor.
			listResults.Items.Add("Tez inceleme işlemi başladı. Lütfen bekleyiniz!");//Sonuç listesine ilk mesajı yazdırıyoruz.
			thesisProcess.RunWorkerAsync();//thesisProcess adında bir thread başlatıyoruz.
		}

		private void thesisProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

			listResults.Items.Add("Tez inceleme bitti!");
			this.Text = "Fırat Üniversitesi Tez Kontrol Aracı";
			bn_start.Enabled = true;
			bn_openThesis.Enabled = true;

		}

		private void thesisProcess_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{

			//report progresten gönderilen değer alınır ve ekranın üst kısmına bastırılır.
			this.Text = "Fırat Üniversitesi Tez Kontrol Aracı %" + e.ProgressPercentage;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
		}
	}
}
