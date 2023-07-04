/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example02
{
    public class Example02 : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;
        [SerializeField] Button prevCellButton = default;
        [SerializeField] Button nextCellButton = default;
        [SerializeField] Text selectedItemInfo = default;
        [SerializeField] private GameController gameController;
        private GameChooser _gameChooser;

        [SerializeField] private List<GameData> gameDescriptions;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Text startGameText;

        [SerializeField] private GameConditionsSetupper gameConditionsSetupper;
        private GameData _curGameData;
        [System.Serializable]
        public struct GameData
        {
            public string gameName;
            public Sprite gameNameImage;
            public Sprite gameIcon;
            public string gameDescription;
            public bool gameReady;
        }
        void Start()
        {
            _gameChooser = GetComponent<GameChooser>();
            //prevCellButton.onClick.AddListener(scrollView.SelectPrevCell);
            //nextCellButton.onClick.AddListener(scrollView.SelectNextCell);
            scrollView.OnSelectionChanged(OnSelectionChanged);

            var items = Enumerable.Range(0, 6)
                .Select(i => new ItemData(gameDescriptions[i].gameName))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }

        public void SendCurGameData()
        {
            gameController.SetGameData(_curGameData);
            _gameChooser.ChooseGameType(_curGameData);
            gameConditionsSetupper.SetGameData(_curGameData);
        }
        void OnSelectionChanged(int index)
        {
            selectedItemInfo.text = gameDescriptions[index].gameDescription;
            startGameButton.interactable = gameDescriptions[index].gameReady;
            if (gameDescriptions[index].gameReady)
            {
                startGameText.text = "Start Game";
                _curGameData = gameDescriptions[index];
            }
            else
            {
                startGameText.text = "Coming Soon";

            }
        }
    }
}
