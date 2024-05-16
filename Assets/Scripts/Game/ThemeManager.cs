using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExordiumGamesAssignment.Scripts.Game
{
    public enum Theme { Dark, Light }

    public class ThemeManager : MonoBehaviour
    {
        public static ThemeManager Instance { get; private set; }

        private readonly string THEME = "Theme";
        public Theme CurrentTheme { get; private set; } = Theme.Dark;

        [SerializeField] private Color darkImageColor;
        [SerializeField] private Color darkTextAndToggleFillColor;
        [SerializeField] private Color darkButtonAndToggleBackgroundColor;
        [SerializeField] private Color darkOutlineColor;

        [SerializeField] private Color lightImageColor;
        [SerializeField] private Color lightTextAndToggleFillColor;
        [SerializeField] private Color lightButtonAndToggleBackgroundColor;
        [SerializeField] private Color lightOutlineColor;

        [SerializeField] private List<Image> imageBackgrounds;
        [SerializeField] private List<TextMeshProUGUI> textElements;
        [SerializeField] private List<Button> buttons;
        [SerializeField] private List<Outline> outlines;
        [SerializeField] private List<Toggle> toggles;

        private Theme changedTheme = Theme.Dark;

        private void Awake()
        {
            Instance = this;

            if (PlayerPrefs.HasKey(THEME))
            {
                Enum.TryParse(PlayerPrefs.GetString(THEME), out Theme theme);
                CurrentTheme = theme;
                changedTheme = theme;
            }
            else
            {
                changedTheme = Theme.Light;
                CurrentTheme = Theme.Light;
                SaveDefaultTheme();
            }
        }

        private void Start()
        {
            ApplyTheme(CurrentTheme);
        }

        private void ApplyTheme(Theme theme)
        {
            switch (theme)
            {
                case Theme.Dark:
                    SetImageBackgroundColors(darkImageColor);
                    SetTextAndToggleFillColors(darkTextAndToggleFillColor);
                    SetButtonAndToggleColors(darkButtonAndToggleBackgroundColor);
                    SetOutlineColors(darkTextAndToggleFillColor);
                    break;
                case Theme.Light:
                    SetImageBackgroundColors(lightImageColor);
                    SetTextAndToggleFillColors(lightTextAndToggleFillColor);
                    SetButtonAndToggleColors(lightButtonAndToggleBackgroundColor);
                    SetOutlineColors(lightTextAndToggleFillColor);
                    break;
            }
        }

        private void SetImageBackgroundColors(Color color)
        {
            foreach (Image image in imageBackgrounds)
            {
                image.color = color;
            }
        }

        private void SetTextAndToggleFillColors(Color color)
        {
            foreach (TextMeshProUGUI text in textElements)
            {
                text.color = color;
            }
        }

        private void SetButtonAndToggleColors(Color color)
        {
            foreach (Button button in buttons)
            {
                ColorBlock colors = button.colors;
                colors.normalColor = color;
                colors.highlightedColor = color;
                colors.selectedColor = Color.white;
                colors.disabledColor = color;
                button.colors = colors;
            }

            foreach (Toggle toggle in toggles)
            {
                ColorBlock colors = toggle.colors;
                colors.normalColor = color;
                colors.highlightedColor = color;
                colors.selectedColor = Color.gray;
                colors.disabledColor = color;
                toggle.colors = colors;
            }
        }

        private void SetOutlineColors(Color color)
        {
            foreach (Outline outline in outlines)
            {
                outline.effectColor = color;
            }
        }

        public void AddTextElements(List<TextMeshProUGUI> textElements)
        {
            this.textElements.AddRange(textElements);
        }

        public void AddTextElement(TextMeshProUGUI textElement)
        {
            textElements.Add(textElement);
        }

        public void AddToggle(Toggle toggle)
        {
            toggles.Add(toggle);
        }

        public void AddImage(Image image)
        {
            imageBackgrounds.Add(image);
        }

        public void AddOutline(Outline outline)
        {
            outlines.Add(outline);
        }

        public void SetTheme(Theme theme)
        {
            changedTheme = theme;
            ApplyTheme(changedTheme);
        }

        public void SaveDefaultTheme()
        {
            CurrentTheme = changedTheme;
            PlayerPrefs.SetString(THEME, CurrentTheme.ToString());
            PlayerPrefs.Save();
        }

        public void Cancel()
        {
            ApplyTheme(CurrentTheme);
        }
    }
}
