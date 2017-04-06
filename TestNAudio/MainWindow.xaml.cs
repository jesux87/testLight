using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Animation;

using NAudio.Wave;

using TestNAudio.Model.Audio;
using TestNAudio.Model.Light;
using TestNAudio.Model.Sequence;
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
        private RelayCommand openFileCommand;

        private RelayCommand blackOutCommand;

        private RelayCommand shutUpCommand;
        
        public MainWindow()
        {
            this.InitCommands();

            this.InitControleurs();

            this.InitializeComponent();
            
            this.RootGrid.InputBindings.Add(new InputBinding(this.goPrevCmd, new KeyGesture(Key.PageUp)));
            this.RootGrid.InputBindings.Add(new InputBinding(this.goNextCmd, new KeyGesture(Key.PageDown)));

            var seq = new Storyboard();
            var avs = new LightSequence(new LightProvider(1));
            //avs.BeginTime = TimeSpan.Zero;
            //avs.Duration = new Duration(TimeSpan.FromSeconds(20));

            //seq.Children.Add(avs);
            seq.Begin();
            //this.Tests();
        }

        private void Tests()
        {
            var vm = new ViewModel();
            vm.OpenFile();
        }

        private void InitControleurs()
        {
            this.Controleurs = new List<IValueControleur>()
                                   {
                                       new FaderValueControleur(KeyboardControlMapping.AQ_KeyboardLine()),
                                       new FaderValueControleur(KeyboardControlMapping.ZS_KeyboardLine()),
                                       new FaderValueControleur(KeyboardControlMapping.ED_KeyboardLine()),
                                       new FaderValueControleur(KeyboardControlMapping.RF_KeyboardLine()),
                                       new FaderValueControleur(KeyboardControlMapping.TG_KeyboardLine()),
                                       new FaderValueControleur(KeyboardControlMapping.YH_KeyboardLine()),
                                       //new FaderValueControleur(KeyboardControlMapping.UJ_KeyboardLine()),
                                       //new FaderValueControleur(KeyboardControlMapping.IK_KeyboardLine()),
                                       //new FaderValueControleur(KeyboardControlMapping.OL),
                                       //new FaderValueControleur(KeyboardControlMapping.PM)
                                       //new AnimationValueControleur(new KeyGesture(Key.NumPad0)) { Value = 1, CurveDirection = CurveDirection.Descendant, Duration = new Duration(TimeSpan.FromSeconds(3)), EasingFunction = new BounceEase() { Bounciness = 5, Bounces = 4, EasingMode = EasingMode.EaseOut } }
                                   };

            this.Projectors = new List<object>();

            var rgb = new LightRgbProvider(3, 4, 5);
            var r = new LightManualSimpleFader(new FaderValueControleur(KeyboardControlMapping.YH_KeyboardLine()), rgb.Red, "Rouge");
            var g = new LightManualSimpleFader(new FaderValueControleur(KeyboardControlMapping.YH_KeyboardLine()), rgb.Green, "Vert");
            var mult = new LightManualMultipleFader(new FaderValueControleur(KeyboardControlMapping.UJ_KeyboardLine()), new[] { new LightProvider(1), new LightProvider(2), rgb.Blue});
            var manF1 = new LightManualSimpleFader(new FaderValueControleur(KeyboardControlMapping.OL_KeyboardLine(KeyboardControlMode.ThreeThirds)), mult.LightProviders[0]);
            this.manF3 = new LightManualSimpleFader(new FaderValueControleur(KeyboardControlMapping.IK_KeyboardLine()), mult.LightProviders[1]);
            var manF2 = new LightManualSimpleFader(new FaderValueControleur(KeyboardControlMapping.PM_KeyboardLine()), manF1);
            //this.autoManF3 = new LightActionAnimation(new AnimationValueControleur(new KeyGesture(Key.NumPad1))
            //                                             {
            //                                                 Value = 1,
            //                                                 CurveDirection = CurveDirection.Descendant,
            //                                                 Duration = new Duration(TimeSpan.FromSeconds(3)),
            //                                                 EasingFunction = new BounceEase()
            //                                                                      {
            //                                                                          Bounciness = 5,
            //                                                                          Bounces = 8,
            //                                                                          EasingMode = EasingMode.EaseOut
            //                                                                      }
            //                                             }, manF2);
            

            this.LightControllers = new List<ILightController>
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
                                          this.autoManF3
                                      };

            this.AudioControllers = new ObservableCollection<IAudioProvider>();
            
            foreach (var lightProvider in LightControllers)
            {
                var manValCtl = lightProvider as LightManualSimpleFader;
                var multValCtl = lightProvider as LightManualMultipleFader;
                var animValCtl = lightProvider as LightActionAnimation;
                if (manValCtl != null)
                {
                    this.InputBindings.AddRange(manValCtl.ValueControleur.InputBindings);
                }
                if (multValCtl != null)
                {
                    this.InputBindings.AddRange(multValCtl.ValueControleur.InputBindings);
                }
                //if (animValCtl != null)
                //{
                //    this.InputBindings.AddRange(animValCtl.AnimControleur.InputBindings);
                //}
            }

            this.Projectors.Add(new BulbProjector(this.autoManF3));
            this.Projectors.Add(new BulbProjector(this.manF3));
            this.Projectors.Add(new RgbProjector(r, g, mult.LightProviders[2]));
        }
        
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
                
                this.WavesProvider = selFiles.Select(s => new WaveChannel32(new AudioFileReader(s))).ToArray();

                foreach (var wav in this.WavesProvider)
                {
                    var ap = new AudioProvider(wav);
                    var av = new AudioVolume(ap);
                    var pan = new AudioPan(av);
                    this.AudioControllers.Add(ap);
                    this.AudioControllers.Add(av);
                    this.AudioControllers.Add(pan);
                    this.AudioControllers.Add(new AudioEqualizer(pan));
                }
            }
        }

        private void InitCommands()
        {
            this.goNextCmd = new RelayCommand(o => this.GoNext());
            this.goPrevCmd = new RelayCommand(o => this.GoPrevious());
            this.openAudioCmd = new RelayCommand(o => this.OpenAudioFile());

            this.openFileCommand = new RelayCommand(o =>
            {
                var ofd = new OpenFileDialog();
                ofd.ShowDialog(this);

                if (ofd.FileName.EndsWith(".xps"))
                {
                    this.PdfViewer.Visibility = Visibility.Hidden;
                    this.XpsViewer.Visibility = Visibility.Visible;
                    this.XpsViewer.Document = new XpsDocument(ofd.FileName, FileAccess.Read).GetFixedDocumentSequence();
                }
                else if (ofd.FileName.EndsWith(".pdf"))
                {
                    this.XpsViewer.Visibility = Visibility.Hidden;
                    this.PdfViewer.Visibility = Visibility.Visible;
                    this.PdfViewer.OpenFile(ofd.FileName);

                    this.PdfViewer.Loaded += (sender, args) =>
                        {
                            this.PdfViewer.ZoomToWidth();
                            this.PdfViewer.PageRowDisplay = PageRowDisplayType.ContinuousPageRows;
                        };
                }
            });

            this.blackOutCommand = new RelayCommand(active => this.BlackOut((bool)active));
            this.shutUpCommand = new RelayCommand(active => this.ShutUp((bool)active));
        }

        private void ShutUp(bool active)
        {
            foreach (var audioProvider in this.AudioControllers.OfType<AudioProvider>())
            {
                audioProvider.OutPutWave.Stop();
            }
        }

        private void BlackOut(bool active)
        {
            foreach (var lightProvider in this.LightControllers.OfType<LightProvider>())
            {
                lightProvider.BlackOut = active;
            }
        }

        private void GoNext()
        {
            this.PdfViewer.GotoNextPage();
            this.XpsViewer.NextPage();
        }

        private void GoPrevious()
        {
            this.PdfViewer.GotoPreviousPage();
            this.XpsViewer.PreviousPage();
        }
        
        public ICommand OpenFileCommand
        {
            get
            {
                return this.openFileCommand;
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

        public IEnumerable<IValueControleur> Controleurs { get; set; }

        public IEnumerable<ILightController> LightControllers { get; set; }

        public IList<IAudioProvider> AudioControllers { get; set; }

        public IList<object> Projectors { get; set; }

        public ICommand OpenAudioCmd
        {
            get
            {
                return this.openAudioCmd;
            }
        }

        public ICommand Play1AudioCmd
        {
            get
            {
                return this.play1AudioCmd;
            }
        }

        public ICommand Play2AudioCmd
        {
            get
            {
                return this.play2AudioCmd;
            }
        }

        public ILightController AutoManF3
        {
            get
            {
                return this.autoManF3;
            }
        }
        public ILightController ManF3
        {
            get
            {
                return this.manF3;
            }
        }
    }
}
