using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EbruAktasIleSayiTahminOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rand = new Random(); 
        int[] rakamlar = new int[4]; //Rakamları tutacak dizi 
        int uretilenSayi = 1000;  //Üretilen sayıyı 1000 değerini ata
        int artiIpucu = 0;    //Başlangıçta artiIpucu ve eksiIpucu puanları 0
        int eksiIpucu = 0;
        int tahminSayisiSayaci = 0;  //tahminSayisiSayaci adlı sayaç belirliyoruz, kaç tahminde bulunduğumuzu tutacak

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Ebru Aktaş ile Sayı Tahmin Oyunu";  //Formun üstünde yazacak başlık
            maskedTextBox1.Mask = "0000";  //4 tane sayı değeri girilebilir şeklinde şart koyduk
            EbruOyunuBaslat();
        }
        private void EbruOyunuBaslat()
        {

            uretilenSayi = rand.Next(1000, 9999); // 1000 ile 9999 arasında random sayı oluşturmak için

            tahminSayisiSayaci = 0;// oyun başladığında ise tahmin sayacı sıfırlanması için
            maskedTextBox1.Text = ""; //oyun başladığında maskedTextBoxa girilen değer sıfırlanması için


            label5.Text = "";
            while (!Control(uretilenSayi))
            {
                uretilenSayi = rand.Next(1000, 9999); //1000 ile 9999 arasında rastgele sayı üretilsin
            }
            for (int i = 0; i < uretilenSayi.ToString().Length; i++)
            {
                rakamlar[i] = Convert.ToInt32(uretilenSayi.ToString()[i].ToString());
            }
            // MessageBox.Show(uretilenSayi.ToString());
        }

        //Rakamların aynı olmamasını sağlıyoruz
        private bool Control(int uretilenSayi)                                                  
        {
            bool control = false;
            string s = uretilenSayi.ToString();
            int[] rakamlar = new int[4];
            for (int i = 0; i < s.Length; i++)
            {
                rakamlar[i] = Convert.ToInt32(s[i].ToString());
            }

            for (int i = 0; i < rakamlar.Length; i++)
            {
                for (int j = i; j < rakamlar.Length; j++)
                {
                    if (rakamlar[i] == rakamlar[j] && i != j)
                    {
                        control = true;
                        break;
                    }
                }
            }
            if (control)
            {
                control = false;
            }
            else
            {
                control = true;
            }
            return control;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Control(Convert.ToInt32(maskedTextBox1.Text)))
            {
                MessageBox.Show("Lütfen rakamları farklı bir sayı giriniz..");
                return;
            }
            int[] alinanSayiRakamlari = new int[4];
            for (int i = 0; i < maskedTextBox1.Text.Length; i++)
            {  //maskedtextBox a girilen değerleri alinanSayiRakamlari adlı dizide tutuyoruz

                alinanSayiRakamlari[i] = Convert.ToInt32(maskedTextBox1.Text[i].ToString());
            }
            artiIpucu = ArtiIpucuKontrolEtEbru(alinanSayiRakamlari);
            eksiIpucu = EksiIpucuKontrolEtEbru(alinanSayiRakamlari);
            label5.Text = "Artı ipucu = " + artiIpucu + " Eksi İpuçları = " + eksiIpucu;
            tahminSayisiSayaci += 1;

            if (artiIpucu == 4)  //Artı puan 4 olursa yani her rakam doğruysa aynı zamanda her rakamın yeri doğruysa random sayıyı bildik demektir.Bunu kullanıcıya bildirdim.
            {
                MessageBox.Show(tahminSayisiSayaci + " denemede tahmin ettiniz..");

                EbruOyunuBaslat();
            }

            label3.Text = tahminSayisiSayaci.ToString();
        }
        private int ArtiIpucuKontrolEtEbru(int[] alinanRakamlar) //Bu metot ile bizim girdiğimiz sayıda ve random sayıdaki rakam doğruysa ve aynı zamanda yeri doğruysa artı puan kısmını artırdım.
        {
            int arti = 0;
            for (int i = 0; i < alinanRakamlar.Length; i++)
            {
                for (int j = 0; j < rakamlar.Length; j++)
                {
                    if (i == j && alinanRakamlar[i] == rakamlar[j])
                    {
                        arti += 1;
                    }
                }
            }
            return arti;
        }
        private int EksiIpucuKontrolEtEbru(int[] alinanRakamlar)    //Bu metot ile sayımızda doğru rakam varsa fakat yeri yanlışsa eksi puan kısmını artırdım.
        {
            int eksi = 0;
            for (int i = 0; i < alinanRakamlar.Length; i++)
            {
                for (int j = 0; j < rakamlar.Length; j++)
                {
                    if (alinanRakamlar[i] == rakamlar[j] && i != j)
                    {
                        eksi += 1;
                    }
                }
            }
            return eksi;
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)   //tuşlarda sadece rakamlara basılabilmesine izin verdim.
            {
                e.Handled = false; //tuşlara basılmasına izin veriyoruz.
            }
            else
            {
                e.Handled = true; //izin vermiyoruz.
            }
        } 

        private void maskedTextBox1_MaskChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length != 4)
            {
                errorProvider1.SetError(maskedTextBox1, "Girilen sayı 4 haneli olmalı.(1000-9999)");
            }
            else
            {

                errorProvider1.Clear();
            }
        }
      }
}
