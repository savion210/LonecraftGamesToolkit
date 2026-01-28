namespace LonecraftGames.Toolkit.Core.Utilis
{
    public abstract class Enums
    {
        public enum UIPanelType
        {
            MainMenu = 0,
            LobbyList = 1,
            LobbyCreate = 2,
            Game = 3,
            Loading = 4,
            Settings = 5,
            Pause = 6,
            LoadingAssets = 7,
            GameLogo = 8,
            MapSelection = 9,
            VivoxLogin = 10,
            VivoxChat = 11,
            ReadyUp = 12,
            LevelPanel = 13,
            LoadMenu = 14,
        }

        public enum SubPanelType
        {
            None = 0,
            GeneralSettings = 1,
            ControlSettings = 2,
            GraphicSettings = 3,
            Inventory = 4,
            SessionMessenger = 5,
            AudioSettings = 6,
            LobbyCreate = 7,
            LobbyJoin = 8,
            VivoxSettings = 9,
            SaveMenu = 10,
            LoadMenu = 11,
            UpgradeMenu = 12,
        }

        public enum UIPopupType
        {
            None = 0,
            LobbyCreate = 1,
            LobbyJoin = 2,
            EULA = 3,
            PrivacyPolicy = 4,
            Confirmation = 5,
        }

        public enum GraphicsQuality
        {
            High,
            Medium,
            Low
        }

        public enum Language
        {
            English,
            Spanish,
            French,
            German,
            Italian,
            Japanese,
            Korean,
            Portuguese,
            Russian,
            ChineseSimplified,
            ChineseTraditional
        }

        public enum LocalizationWords
        {
            Play,
            Settings,
            Credits,
            Quit,
            LobbyName,
            Players,
            Refresh,
            Create,
            CreateLobby,
            Cancel,
            Loading,
            General,
            Graphics,
            Language,
            Controls,
            Back,
            Resolution,
            MasterVolume,
            Music,
            Ambient,
            Effects,
            PauseMenu,
            Resume,
            Inventory,
            Keys,
            Books,
            Health,
            MapSelection,
            Close,
            LobbyCode,
            English,
            Spanish,
            French,
            German,
            Italian,
            Japanese,
            Korean,
            Portuguese,
            Russian,
            ChineseSimplified,
            ChineseTraditional,
            Audio,
            Join,
 
        }
        public enum DoorUnlockType
        {
            Purchase,
            Key,
            Puzzle,
            Padlock
        }
        
        public enum GameState
        {
            MainMenu,
            Game,
            Pause,
            GameOver
        }
        
        public enum AudiopClipEnum
        {
            MenuPanel,
            GameOver,
            ButtonClick,
            Ambience1,
            Ambience2,
            BackgroundMusic1,
            BackgroundMusic2,
            MaleScream1,
            MaleScream2,
            MonsterGrowl1,
            MonsterGrowl2,
            HitEffect1,
            HitEffect2,
            HitEffect3

        }
        
        public enum SoundCategory
        {
            Master,
            Music,
            Ambient,
            Effects
        }
    }
}