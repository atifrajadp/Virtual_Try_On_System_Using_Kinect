using Virtual_Try_On_System.Model.ClothingItems;
using Virtual_Try_On_System.View_Model.ButtonItems;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Media;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer;
using Virtual_Try_On_System.Database;

namespace Virtual_Try_On_System.View_Model
{
    public class KinectViewModel : ViewModelBase
    {
       
        // The clothing manager
        
        private ClothingManager _clothingManager;
       
        // The Kinect service
       
        private readonly KinectService _kinectService;


       
        // Gets the button player.
       
        public static SoundPlayer ButtonPlayer { get; private set; }
       
        // Gets or sets the clothing manager.
       
        public ClothingManager ClothingManager
        {
            get { return _clothingManager; }
            set
            {
                if (_clothingManager == value)
                    return;
                _clothingManager = value;
                OnPropertyChanged("ClothingManager");
            }
        }
        
        // Gets the kinect service.
      
        public KinectService KinectService
        {
            get { return _kinectService; }
        }
       
        // Gets a value indicating whether [debug mode on].
       
        public bool DebugModeOn
        {
            get
            {
                return true;

            }
        }

      
        // Initializes a new instance of the <see cref="KinectViewModel"/> class.
       
        public KinectViewModel(KinectService kinectService)
        {
            ButtonPlayer = new SoundPlayer(Properties.Resources.ButtonClick);
            InitializeClothingCategories();
            _kinectService = kinectService;
            _kinectService.Initialize();
        }

        // Initializes the clothing categories.
        
        private void InitializeClothingCategories()
        {
            ClothingManager.Instance.ClothingCategories = new ObservableCollection<ClothingCategoryButtonViewModel>
            {
                      
                CreateShirtsClothingCategoryButton(),
                CreateTrousersClothingCategoryButton()
        
               
            };
            ClothingManager.Instance.UpdateActualCategories();
        }

       
       
        // Creates the Shirts clothing category button.
       
     

        private ClothingCategoryButtonViewModel CreateShirtsClothingCategoryButton()
        {
            string pathformodel = @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\Resources\Models\Shirts\tshirt_coral_blue.obj";
            string pathformaterial = @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\Resources\Models\Shirts\tshirt_coral_blue.mtl";
            string pathforimage = @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\Resources\Materials\tshirt_coral_blue.jpg";
            //Set IDENTITY_INSERT Clothes ON
            
            //string query = "INSERT INTO Clothes(Shirt_name, Shirt_model,Shirt_material_name,Shirt_material,Shirt_Design) VALUES(@Shirt_name, @Shirt_model,@Shirt_material_name,@Shirt_material,@Shirt_Design)";
            //string query = "UPDATE Clothes SET Shirt_name = @Shirt_name, Shirt_model = @Shirt_model,Shirt_material_name = @Shirt_material_name,Shirt_material = @Shirt_material,Shirt_Design = @Shirt_Design WHERE Id=2";
            //Datainsertion dt = new Datainsertion(pathformodel, pathformaterial, pathforimage, query, "@Shirt_name", "@Shirt_model", "@Shirt_material_name", "@Shirt_material", "@Shirt_Design");
            //Dataretrieval obj1 = new Dataretrieval("tshirt_navy.obj",@"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\bin\Debug\Resources\Models\Shirts\","SELECT Shirt_model FROM CLOTHES WHERE Shirt_name=@Shirt_name ","@Shirt_name");
            //Dataretrieval obj2 = new Dataretrieval("tshirt_navy.mtl", @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\bin\Debug\Resources\Models\Shirts\", "SELECT Shirt_material FROM CLOTHES WHERE Shirt_material_name=@Shirt_material_name ", "@Shirt_material_name");
            //Dataretrieval obj3 = new Dataretrieval("tshirt_navy.jpg", @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\bin\Debug\Resources\Materials\", "SELECT Shirt_Design FROM CLOTHES WHERE Shirt_Design_name=@Shirt_Design_name ", "@Shirt_Design_name");

            //Dataretrieval obj4 = new Dataretrieval();
            //Dataretrieval obj5 = new Dataretrieval("tshirt_coral_blue.mtl", @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\bin\Debug\Resources\Models\Shirts\", "SELECT Shirt_material FROM CLOTHES WHERE Shirt_material_name=@Shirt_material_name ", "@Shirt_material_name");
            //Dataretrieval obj6 = new Dataretrieval("tshirt_coral_blue.jpg", @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\bin\Debug\Resources\Materials\", "SELECT Shirt_Design FROM CLOTHES WHERE Shirt_Design_name=@Shirt_Design_name ", "@Shirt_Design_name");

            //Dataretrieval obj7 = new Dataretrieval("tshirt_green_blue.obj", @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\bin\Debug\Resources\Models\Shirts\", "SELECT Shirt_model FROM CLOTHES WHERE Shirt_name=@Shirt_name ", "@Shirt_name");
            //Dataretrieval obj8 = new Dataretrieval("tshirt_green_blue.mtl", @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\bin\Debug\Resources\Models\Shirts\", "SELECT Shirt_material FROM CLOTHES WHERE Shirt_material_name=@Shirt_material_name ", "@Shirt_material_name");
            //Dataretrieval obj9 = new Dataretrieval("tshirt_green_blue.jpg", @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\bin\Debug\Resources\Materials\", "SELECT Shirt_Design FROM CLOTHES WHERE Shirt_Design_name=@Shirt_Design_name ", "@Shirt_Design_name");
            return new ClothingCategoryButtonViewModel(ClothingItemBase.ClothingType.ShirtItem)
            {
                
                Image = Properties.Resources.shirt_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,   @"Resources\Models\Shirts\tshirt_navy.obj") 
                    { Image =Properties.Resources.tshirt_navy ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,   @"Resources\Models\Shirts\tshirt_coral_blue.obj") 
                    { Image =Properties.Resources.tshirt_coral_blue ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\tshirt_green_blue.obj") 
                    { Image =Properties.Resources.tshirt_green_blue ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\tshirt_olive.obj") 
                    { Image =Properties.Resources.tshirt_olive ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\tshirt_orange_blue.obj") 
                    { Image =Properties.Resources.tshirt_orange_blue ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n1.obj") 
                    { Image =Properties.Resources.n1 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n2.obj") 
                    { Image =Properties.Resources.n2 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n3.obj") 
                    { Image =Properties.Resources.n3 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n4.obj") 
                    { Image =Properties.Resources.n4 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n6.obj") 
                    { Image =Properties.Resources.n6 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n7.obj") 
                    { Image =Properties.Resources.n7 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n8.obj") 
                    { Image =Properties.Resources.n8 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n11.obj") 
                    { Image =Properties.Resources.n11 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n12.obj") 
                    { Image =Properties.Resources.n12 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n15.obj") 
                    { Image =Properties.Resources.n15 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n16.obj") 
                    { Image =Properties.Resources.n16 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n18.obj") 
                    { Image =Properties.Resources.n18 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n19.obj") 
                    { Image =Properties.Resources.n19,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n20.obj") 
                    { Image =Properties.Resources.n20,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n22.obj") 
                    { Image =Properties.Resources.n22 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n23.obj") 
                    { Image =Properties.Resources.n23 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n26.obj") 
                    { Image =Properties.Resources.n26 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n27.obj") 
                    { Image =Properties.Resources.n27 ,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n28.obj") 
                    { Image =Properties.Resources.n28,Ratio = 1.45 },
                    new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\n29.obj") 
                    { Image =Properties.Resources.n29 ,Ratio = 1.45 },
                     new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\1.obj") 
                    { Image =Properties.Resources.r1 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\2.obj") 
                    { Image =Properties.Resources.r2 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\3.obj") 
                    { Image =Properties.Resources.r3,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\5.obj") 
                    { Image =Properties.Resources.r5 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\6.obj") 
                    { Image =Properties.Resources.r6 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\8.obj") 
                    { Image =Properties.Resources.r8 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\11.obj") 
                    { Image =Properties.Resources.r11 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\12.obj") 
                    { Image =Properties.Resources.r12 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\13.obj") 
                    { Image =Properties.Resources.r13 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\14.obj") 
                    { Image =Properties.Resources.r14 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\15.obj") 
                    { Image =Properties.Resources.r15 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\16.obj") 
                    { Image =Properties.Resources.r16 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\17.obj") 
                    { Image =Properties.Resources.r17 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\18.obj") 
                    { Image =Properties.Resources.r18,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\19.obj") 
                    { Image =Properties.Resources.r19,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\21.obj") 
                    { Image =Properties.Resources.r21 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\22.obj") 
                    { Image =Properties.Resources.r22 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\23.obj") 
                    { Image =Properties.Resources.r23 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\24.obj") 
                    { Image =Properties.Resources.r24 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\25.obj") 
                    { Image =Properties.Resources.r25 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\27.obj") 
                    { Image =Properties.Resources.r27 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\28.obj") 
                    { Image =Properties.Resources.r28 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\29.obj") 
                    { Image =Properties.Resources.r29,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\30.obj") 
                    { Image =Properties.Resources.r30 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\32.obj") 
                    { Image =Properties.Resources.r32 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\33.obj") 
                    { Image =Properties.Resources.r33 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\34.obj") 
                    { Image =Properties.Resources.r34,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\35.obj") 
                    { Image =Properties.Resources.r35,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\38.obj") 
                    { Image =Properties.Resources.r38 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\39.obj") 
                    { Image =Properties.Resources.r39 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\40.obj") 
                    { Image =Properties.Resources.r40 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\41.obj") 
                    { Image =Properties.Resources.r41 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\42.obj") 
                    { Image =Properties.Resources.r42 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\43.obj") 
                    { Image =Properties.Resources.r43 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\44.obj") 
                    { Image =Properties.Resources.r44,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\45.obj") 
                    { Image =Properties.Resources.r45,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\46.obj") 
                    { Image =Properties.Resources.r46 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\47.obj") 
                    { Image =Properties.Resources.r47 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\48.obj") 
                    { Image =Properties.Resources.r48 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\49.obj") 
                    { Image =Properties.Resources.r49 ,Ratio = 1.45 },
                      new ShirtButtonViewModel(ClothingItemBase.ClothingType.ShirtItem,  @"Resources\Models\Shirts\50.obj") 
                    { Image =Properties.Resources.r50 ,Ratio = 1.45 },
                       
                }
                
            };
           
        }

     
        // Creates the Trousers clothing category button.
       
        
        private ClothingCategoryButtonViewModel CreateTrousersClothingCategoryButton()
        {
            string pathformodel = @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\Resources\Models\Pants\pant_green.obj";
            string pathformaterial = @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\Resources\Models\Pants\pant_green.mtl";
            string pathforimage = @"G:\7th Semester\CS-490 Final Year Project\Virtual Try On System\Virtual Try On System\Resources\Materials\Pant_green_stripes.jpg";
            string query = "UPDATE Clothes SET Pant_name = @Pant_name, Pant_model = @Pant_model,Pant_material_name = @Pant_material_name,Pant_material = @Pant_material,Pant_Design = @Pant_Design WHERE Id=3";
            //Datainsertion dt = new Datainsertion(pathformodel, pathformaterial, pathforimage, query, "@Pant_name", "@Pant_model", "@Pant_material_name", "@Pant_material", "@Pant_Design");
            
            
            return new ClothingCategoryButtonViewModel(ClothingItemBase.ClothingType.TrouserItem)
            {
              
                Image = Properties.Resources.trouser_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                   
                    new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\pant_jeans.obj") 
                    { Image =  Properties.Resources.pant_jeans },
                     new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\pant_denim.obj") 
                    { Image =  Properties.Resources.pant_denim  },
                     new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\pant_green.obj") 
                    { Image =  Properties.Resources.pant_green },
                     new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\1.obj") 
                    { Image =  Properties.Resources._1 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\2.obj") 
                    { Image =  Properties.Resources._2 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\3.obj") 
                    { Image =  Properties.Resources._3 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\4.obj") 
                    { Image =  Properties.Resources._4 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\5.obj") 
                    { Image =  Properties.Resources._5 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\6.obj") 
                    { Image =  Properties.Resources._6 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\7.obj") 
                    { Image =  Properties.Resources._7 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\8.obj") 
                    { Image =  Properties.Resources._8 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\9.obj") 
                    { Image =  Properties.Resources._9 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\10.obj") 
                    { Image =  Properties.Resources._10 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\12.obj") 
                    { Image =  Properties.Resources._12 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\13.obj") 
                    { Image =  Properties.Resources._13},
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\14.obj") 
                    { Image =  Properties.Resources._14 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\15.obj") 
                    { Image =  Properties.Resources._15},
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\16.obj") 
                    { Image =  Properties.Resources._16},
                         new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\19.obj") 
                    { Image =  Properties.Resources._19 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\20.obj") 
                    { Image =  Properties.Resources._20 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\21.obj") 
                    { Image =  Properties.Resources._21 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\22.obj") 
                    { Image =  Properties.Resources._22},
                     new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\23.obj") 
                    { Image =  Properties.Resources._23},
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\24.obj") 
                    { Image =  Properties.Resources._24 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\25.obj") 
                    { Image =  Properties.Resources._25 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\26.obj") 
                    { Image =  Properties.Resources._26},
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\27.obj") 
                    { Image =  Properties.Resources._27 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\28.obj") 
                    { Image =  Properties.Resources._28 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\29.obj") 
                    { Image =  Properties.Resources._29 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\30.obj") 
                    { Image =  Properties.Resources._30},
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\32.obj") 
                    { Image =  Properties.Resources._32 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\33.obj") 
                    { Image =  Properties.Resources._33},
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\34.obj") 
                    { Image =  Properties.Resources._34 },                   
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\36.obj") 
                    { Image =  Properties.Resources._36 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\37.obj") 
                    { Image =  Properties.Resources._37 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\38.obj") 
                    { Image =  Properties.Resources._38 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\39.obj") 
                    { Image =  Properties.Resources._39 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\40.obj") 
                    { Image =  Properties.Resources._40 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\51.obj") 
                    { Image =  Properties.Resources._51 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\52.obj") 
                    { Image =  Properties.Resources._52 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\54.obj") 
                    { Image =  Properties.Resources._54 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\55.obj") 
                    { Image =  Properties.Resources._55 },                   
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\57.obj") 
                    { Image =  Properties.Resources._57 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\58.obj") 
                    { Image =  Properties.Resources._58 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\59.obj") 
                    { Image =  Properties.Resources._59 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\60.obj") 
                    { Image =  Properties.Resources._60 },                    
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\62.obj") 
                    { Image =  Properties.Resources._62 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\63.obj") 
                    { Image =  Properties.Resources._63 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\64.obj") 
                    { Image =  Properties.Resources._64 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\65.obj") 
                    { Image =  Properties.Resources._65 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\66.obj") 
                    { Image =  Properties.Resources._66 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\67.obj") 
                    { Image =  Properties.Resources._67 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\68.obj") 
                    { Image =  Properties.Resources._68 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\69.obj") 
                    { Image =  Properties.Resources._69 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\70.obj") 
                    { Image =  Properties.Resources._70},
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\71.obj") 
                    { Image =  Properties.Resources._71 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\72.obj") 
                    { Image =  Properties.Resources._72 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\73.obj") 
                    { Image =  Properties.Resources._73 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\74.obj") 
                    { Image =  Properties.Resources._74 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\75.obj") 
                    { Image =  Properties.Resources._75 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\76.obj") 
                    { Image =  Properties.Resources._76 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\77.obj") 
                    { Image =  Properties.Resources._77},
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\78.obj") 
                    { Image =  Properties.Resources._78 },
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\79.obj") 
                    { Image =  Properties.Resources._79},
                      new TrouserButtonViewModel(ClothingItemBase.ClothingType.TrouserItem,  @"Resources\Models\Pants\80.obj") 
                    { Image =  Properties.Resources._80 }  
                }
            };
        }

    }
}
