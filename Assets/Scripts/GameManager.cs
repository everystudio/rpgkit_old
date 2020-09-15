using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class GameManager : Singleton<GameManager>
    {


        [Header("Currently Owned Items")]
        public string[] itemsHolder;
        public string[] equipItemsHolder;


        [Header("Currently active menus")]
        //Bools for checking if one of these menus is currently active
        public bool cutSceneActive;
        public bool gameMenuOpen;
        public bool dialogActive;
        public bool fadingBetweenAreas;
        public bool shopActive;
        public bool battleActive;
        public bool saveMenuActive;
        public bool innActive;
        public bool itemCharChoiceMenu;
        public bool loadPromt;
        public bool quitPromt;
        public bool itemMenu;
        public bool equipMenu;
        public bool statsMenu;
        public bool skillsMenu;

    }
}



