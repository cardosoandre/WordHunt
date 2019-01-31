using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WordHunt : MonoBehaviour {

    private string[,] lettersGrid;
    private Transform[,] lettersTransforms;

    [Header("Text Asset")]
    public TextAsset wordsSource;
    [Space]

    [Header("List of Words")]
    public List<string> words = new List<string>();
    [Header("Grid Settings")]
    public Vector2 gridSize;
    [Space]

    [Header("Cell Settings")]
    public Vector2 cellSize;
    public Vector2 cellSpacing;
    [Space]

    [Header("Public References")]
    public GameObject letterPrefab;

    private string alphabet = "abcdefghijklmnopqrstuvwxyz";

    void Start()
    {

        PrepareWords();

        InitializeGrid();

        InsertWordsOnGrid();

        RandomizeEmptyCells();
    }


    private void PrepareWords()
    {
        //Pegar lista de palavras
        words = wordsSource.text.Split(',').ToList();

        //Filtrar as palavras que cabem na grid
        int maxGridDimension = Mathf.Max((int)gridSize.x, (int)gridSize.y);

        //Que palavras da lista cabem no grid
        words = words.Where(x => x.Length <= maxGridDimension).ToList();
    }

    private void InitializeGrid()
    {

        //Inicializar o tamanho dos arrays bidimensionais
        lettersGrid = new string[(int)gridSize.x, (int)gridSize.y];
        lettersTransforms = new Transform[(int)gridSize.x, (int)gridSize.y];

        //Passar por todos os elementos x e y da grid
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {

                lettersGrid[x, y] = "";

                GameObject letter = Instantiate(letterPrefab, transform.GetChild(0));

                letter.name = x.ToString() + "-" + y.ToString();

                lettersTransforms[x, y] = letter.transform;

            }
        }

        ApplyGridSettings();
    }

    void ApplyGridSettings()
    {
        GridLayoutGroup gridLayout = transform.GetChild(0).GetComponent<GridLayoutGroup>();

        gridLayout.cellSize = cellSize;
        gridLayout.spacing = cellSpacing;

        int cellSizeX = (int)gridLayout.cellSize.x + (int)gridLayout.spacing.x;

        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(cellSizeX * gridSize.x, 0);
    }

    void InsertWordsOnGrid()
    {
        foreach (string word in words)
        {

            System.Random rn = new System.Random();

            bool inserted = false;
            int tryAmount = 0;

            do
            {
                int row = rn.Next((int)gridSize.x);
                int column = rn.Next((int)gridSize.y);

                int dirX = 0; int dirY = 0;

                while (dirX == 0 && dirY == 0)
                {
                    dirX = rn.Next(3) - 1;
                    dirY = rn.Next(3) - 1;
                }

                inserted = InsertWord(word, row, column, dirX, dirY);
                tryAmount++;

            } while (!inserted && tryAmount < 100);
        }
    }

    private bool InsertWord(string word, int row, int column, int dirX, int dirY)
    {

        if (!CanInsertWordOnGrid(word, row, column, dirX, dirY))
            return false;

        for (int i = 0; i < word.Length; i++)
        {
            lettersGrid[(i * dirX) + row, (i * dirY) + column] = word[i].ToString();
            Transform t = lettersTransforms[(i * dirX) + row, (i * dirY) + column];
            t.GetComponentInChildren<Text>().text = word[i].ToString().ToUpper();
        }
        return true;
    }

    private bool CanInsertWordOnGrid(string word, int row, int column, int dirX, int dirY)
    {
        if (dirX > 0)
        {
            if (row + word.Length > gridSize.x)
            {
                return false;
            }
        }
        if (dirX < 0)
        {
            if (row - word.Length < 0)
            {
                return false;
            }
        }
        if (dirY > 0)
        {
            if (column + word.Length > gridSize.y)
            {
                return false;
            }
        }
        if (dirY < 0)
        {
            if (column - word.Length < 0)
            {
                return false;
            }
        }

        for (int i = 0; i < word.Length; i++)
        {
            string currentCharOnGrid = (lettersGrid[(i * dirX) + row, (i * dirY) + column]);
            string currentCharOnWord = (word[i].ToString());

            if (currentCharOnGrid != String.Empty && currentCharOnWord != currentCharOnGrid)
            {
                return false;
            }
        }

        return true;
    }

    private void RandomizeEmptyCells()
    {

        System.Random rn = new System.Random();

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (lettersGrid[x, y] == string.Empty)
                {
                    lettersGrid[x, y] = alphabet[rn.Next(alphabet.Length)].ToString();
                    lettersTransforms[x, y].GetComponentInChildren<Text>().text = lettersGrid[x, y].ToUpper();
                }
            }
        }
    }
}
