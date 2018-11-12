using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;
using System.Xml.Serialization;

using NAudio.Wave;

using TestNAudio.Model;
using TestNAudio.Model.Audio;
using TestNAudio.Model.Light;
using TestNAudio.Model.XmlModel;
using TestNAudio.ValueControl;

namespace TestNAudio
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Xps.Packaging;
    using Microsoft.Win32;
    using MoonPdfLib;

    using WpfTools;

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private Scene scene;

        private RelayCommand openFileCommand;

        private RelayCommand blackOutCommand;

        private RelayCommand shutUpCommand;

        public MainWindow()
        {
            InitCommands();

            InitControleurs();

            InitializeComponent();

            RootGrid.InputBindings.Add(new InputBinding(goPrevCmd, new KeyGesture(Key.PageUp)));
            RootGrid.InputBindings.Add(new InputBinding(goNextCmd, new KeyGesture(Key.PageDown)));

            var seq = new Storyboard();
            //var avs = new LightSequence(new LightProvider(1));
            //avs.BeginTime = TimeSpan.Zero;
            //avs.Duration = new Duration(TimeSpan.FromSeconds(20));

            //seq.Children.Add(avs);
            seq.Begin();
            //Tests();
            
            TestWriteFile();
            TestReadFile(Path.Combine(Path.GetTempPath(), "bidule.xml"));
        }

        private void TestReadFile(string filePath)
        {
            Scene scene1;
            var got = Scene.TryGet(filePath, out scene1);
        }

        private void TestWriteFile()
        {
            var guids = new List<Guid>()
                            {
                                Guid.NewGuid(),
                                Guid.NewGuid(),
                                Guid.NewGuid(),
                                Guid.NewGuid(),
                                Guid.NewGuid(),
                                Guid.NewGuid()
                            };

            var scene1 = new Scene()
                             {
                                 Name = "Scene de test serialisation",
                                 LightSources = new List<LightSource>()
                                                    {
                                                        new BulbLightSource()
                                                            {
                                                                Channel = 1,
                                                                BulbColor = Colors.Goldenrod,
                                                                Id = guids[0]
                                                            },
                                                        new RgbLightSource()
                                                            {
                                                                BlueChannel = 2,
                                                                GreenChannel = 3,
                                                                RedChannel = 4,
                                                                Id = guids[2]
                                                            }
                                                    },
                                 AudioSources = new List<AudioSource>()
                                                    {
                                                        new AudioSource()
                                                            {
                                                                FilePath = "C:\\USERS\\DV17590N\\Musique\\Iron Maiden\\Brave New World\\Blood Brothers.mp3",
                                                                Id = guids[1]
                                                            }
                                                    },
                                 LightFaders = new List<LightFader>()
                                                   {
                                                       new SingleLightFader()
                                                           {
                                                               Level = 1,
                                                               Value = 0xe8,
                                                               IdSource = guids[0]
                                                           },
                                                       new SingleLightFader()
                                                           {
                                                               Level = 1,
                                                               Value = 0xc5,
                                                               IdSource = guids[2]
                                                           },
                                                       new GroupLightFader()
                                                           {
                                                               Value = 0xff,
                                                               LightFaders = new List<SingleLightFader>()
                                                                                 {
                                                                                     new SingleLightFader()
                                                                                         {
                                                                                             Level = 3,
                                                                                             Value = 0xff,
                                                                                             IdSource = guids[2]
                                                                                         }
                                                                                 }
                                                           }
                                                   },
                                 AudioFaders = new List<AudioFader>()
                                                   {
                                                       new AudioFader()
                                                           {
                                                               IdSource = guids[1],
                                                               Level = 1,
                                                               Value = 1
                                                           }
                                                   },
                                 Sequences = new List<Sequence>()
                                                 {
                                                     new Sequence()
                                                         {
                                                             AudioSequences = new List<AudioSequence>()
                                                                                  {
                                                                                      new AudioSequence()
                                                                                          {
                                                                                              BeginTime = TimeSpan.FromSeconds(.5),
                                                                                              Duration = TimeSpan.FromSeconds(2),
                                                                                              From = 0,
                                                                                              To = 1,
                                                                                              CurvePower = 2,
                                                                                              EasingMode = EasingMode.EaseInOut,
                                                                                              Level = 2,
                                                                                              IdSource = guids[1]
                                                                                          }
                                                                                  },
                                                             LightSequences = new List<LightSequence>()
                                                                                  {
                                                                                      new LightSequence()
                                                                                          {
                                                                                              Duration = TimeSpan.FromSeconds(3),
                                                                                              From = 0,
                                                                                              To = 128,
                                                                                              CurvePower = 4,
                                                                                              EasingMode = EasingMode.EaseIn,
                                                                                              Level = 2,
                                                                                              IdSource = guids[0]
                                                                                          }
                                                                                  }
                                                         }
                                                 }
                             };

            
            var filePath = Path.Combine(Path.GetTempPath(), "bidule.xml");
            var saved = Scene.TrySave(scene1, filePath);
        }

        private void Tests()
        {
            var vm = new ViewModel();
            vm.OpenFile();
        }

        private void InitControleurs()
         {
             
             //Controleurs = new List<IValueControleur>()
             //                       {
             //                           new FaderValueControleur(KeyboardControlMapping.AQ_KeyboardLine()),
             //                           new FaderValueControleur(KeyboardControlMapping.ZS_KeyboardLine()),
             //                           new FaderValueControleur(KeyboardControlMapping.ED_KeyboardLine()),
             //                           new FaderValueControleur(KeyboardControlMapping.RF_KeyboardLine()),
             //                           new FaderValueControleur(KeyboardControlMapping.TG_KeyboardLine()),
             //                           new FaderValueControleur(KeyboardControlMapping.YH_KeyboardLine()),
             //                           //new FaderValueControleur(KeyboardControlMapping.UJ_KeyboardLine()),
             //                           //new FaderValueControleur(KeyboardControlMapping.IK_KeyboardLine()),
             //                           //new FaderValueControleur(KeyboardControlMapping.OL),
             //                           //new FaderValueControleur(KeyboardControlMapping.PM)
             //                           //new AnimationValueControleur(new KeyGesture(Key.NumPad0)) { Value = 1, CurveDirection = CurveDirection.Descendant, Duration = new Duration(TimeSpan.FromSeconds(3)), EasingFunction = new BounceEase() { Bounciness = 5, Bounces = 4, EasingMode = EasingMode.EaseOut } }
             //                       };

             Projectors = new List<object>();

             var rgb = new LightRgbProvider(3, 4, 5);
             var r = new LightManualSimpleFader(new FaderValueControleur(KeyboardLineControlMapping.YH_KeyboardLine()), rgb.Red, "Rouge");
             var g = new LightManualSimpleFader(new FaderValueControleur(KeyboardLineControlMapping.YH_KeyboardLine()), rgb.Green, "Vert");
             var mult = new LightManualMultipleFader(new FaderValueControleur(KeyboardLineControlMapping.UJ_KeyboardLine()), new[] { new LightProvider(1), new LightProvider(2), rgb.Blue});
             var manF1 = new LightManualSimpleFader(new FaderValueControleur(KeyboardLineControlMapping.OL_KeyboardLine(KeyboardLineControlMode.ThreeThirds)), mult.LightProviders[0]);
             manF3 = new LightManualSimpleFader(new FaderValueControleur(KeyboardLineControlMapping.IK_KeyboardLine()), mult.LightProviders[1]);
             var manF2 = new LightManualSimpleFader(new FaderValueControleur(KeyboardLineControlMapping.PM_KeyboardLine()), manF1);
            autoManF3 = new LightActionAnimation(new AnimationValueControleur(new KeyGesture(Key.NumPad1))
            {
                Value = 1,
                CurveDirection = CurveDirection.Descendant,
                Duration = new Duration(TimeSpan.FromSeconds(3)),
                EasingFunction = new BounceEase()
                {
                    Bounciness = 5,
                    Bounces = 8,
                    EasingMode = EasingMode.EaseOut
                }
            }, manF2);


            LightControllers = new List<ILightController>
                                       {
                                           //new LightActionAnimation(new AnimationValueControleur(Key.NumPad0),  new LightProvider(2)),
                                           //new LightProvider(3),
                                           mult,
                                           r,
                                           g,
                                           //new LightTimeAnimation(new LightProvider(4)),
                                           manF1,
                                           manF2,
                                           manF3,
                                           autoManF3
                                       };

             AudioControllers = new List<IAudioProvider>();

             foreach (var lightProvider in LightControllers)
             {
                 var manValCtl = lightProvider as LightManualSimpleFader;
                 var multValCtl = lightProvider as LightManualMultipleFader;
                 var animValCtl = lightProvider as LightActionAnimation;
                 if (manValCtl != null)
                 {
                     InputBindings.AddRange(manValCtl.ValueControleur.InputBindings);
                 }
                 if (multValCtl != null)
                 {
                     InputBindings.AddRange(multValCtl.ValueControleur.InputBindings);
                 }
                 //if (animValCtl != null)
                 //{
                 //    InputBindings.AddRange(animValCtl.AnimControleur.InputBindings);
                 //}
             }

             Projectors.Add(new BulbProjector(autoManF3, Colors.DarkOrange));
             Projectors.Add(new BulbProjector(manF3));
             Projectors.Add(new RgbProjector(r, g, mult.LightProviders[2]));
         }

        public List<ILightController> LightControllers { get; set; }

        public List<IAudioProvider> AudioControllers { get; set; }

        public List<object> Projectors { get; set; }

        public WaveChannel32[] WavesProvider { get; private set; }

        public void OpenAudioFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Supported Files (*.wav;*.mp3)|*.wav;*.mp3|All Files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            bool? flag = openFileDialog.ShowDialog();
            if (flag.HasValue && flag.Value)
            {
                var selFiles = openFileDialog.FileNames;

                WavesProvider = selFiles.Select(s => new WaveChannel32(new AudioFileReader(s))).ToArray();

                foreach (var wav in WavesProvider)
                {
                    var ap = new AudioProvider(wav);
                    var av = new AudioVolume(ap);
                    var pan = new AudioPan(av);
                    /*Scene.AudioControllers.Add(ap);
                    Scene.AudioControllers.Add(av);
                    Scene.AudioControllers.Add(pan);
                    Scene.AudioControllers.Add(new AudioEqualizer(pan));*/
                }
            }
        }

        private void InitCommands()
        {
            goNextCmd = new RelayCommand(o => GoNext());
            goPrevCmd = new RelayCommand(o => GoPrevious());
            openAudioCmd = new RelayCommand(o => OpenAudioFile());

            openFileCommand = new RelayCommand(o =>
            {
                var ofd = new OpenFileDialog();
                ofd.ShowDialog(this);

                if (ofd.FileName.EndsWith(".xps"))
                {
                    PdfViewer.Visibility = Visibility.Hidden;
                    XpsViewer.Visibility = Visibility.Visible;
                    XpsViewer.Document = new XpsDocument(ofd.FileName, FileAccess.Read).GetFixedDocumentSequence();
                }
                else if (ofd.FileName.EndsWith(".pdf"))
                {
                    XpsViewer.Visibility = Visibility.Hidden;
                    PdfViewer.Visibility = Visibility.Visible;
                    PdfViewer.OpenFile(ofd.FileName);

                    PdfViewer.Loaded += (sender, args) =>
                        {
                            PdfViewer.ZoomToWidth();
                            PdfViewer.PageRowDisplay = PageRowDisplayType.ContinuousPageRows;
                        };
                }
            });

            blackOutCommand = new RelayCommand(active => BlackOut((bool)active));
            shutUpCommand = new RelayCommand(active => ShutUp((bool)active));
        }

        private void ShutUp(bool active)
        {
            /*foreach (var audioProvider in Scene.AudioControllers.OfType<AudioProvider>())
            {
                audioProvider.OutPutWave.Stop();
            }*/
        }

        private void BlackOut(bool active)
        {
            /*foreach (var lightProvider in Scene.LightControllers.OfType<LightProvider>())
            {
                lightProvider.BlackOut = active;
            }*/
        }

        private void GoNext()
        {
            PdfViewer.GotoNextPage();
            XpsViewer.NextPage();
        }

        private void GoPrevious()
        {
            PdfViewer.GotoPreviousPage();
            XpsViewer.PreviousPage();
        }

        public ICommand OpenFileCommand
        {
            get
            {
                return openFileCommand;
            }
        }

        private RelayCommand goNextCmd;

        private RelayCommand goPrevCmd;

        public event PropertyChangedEventHandler PropertyChanged;
        private RelayCommand openAudioCmd;
        private RelayCommand play2AudioCmd;
        private RelayCommand play1AudioCmd;
        private ILightProvider autoManF3;
        private ILightProvider manF3;

        //public IEnumerable<IValueControleur> Controleurs { get; set; }

        //public IEnumerable<ILightController> LightControllers { get; set; }

        //public IList<IAudioProvider> AudioControllers { get; set; }

        //public IList<object> Projectors { get; set; }

        public ICommand OpenAudioCmd
        {
            get
            {
                return openAudioCmd;
            }
        }

        public ICommand Play1AudioCmd
        {
            get
            {
                return play1AudioCmd;
            }
        }

        public ICommand Play2AudioCmd
        {
            get
            {
                return play2AudioCmd;
            }
        }

        public ILightController AutoManF3
        {
            get
            {
                return autoManF3;
            }
        }
        public ILightController ManF3
        {
            get
            {
                return manF3;
            }
        }
    }
}
