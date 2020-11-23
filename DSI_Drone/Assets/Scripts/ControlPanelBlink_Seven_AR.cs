﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlPanelBlink_Seven_AR : MonoBehaviour
{
    public const int blink_per_button = 20;
    public const int num_of_button = 7;
    public GameObject ControlPanel;
    public Text txt;
    public int random = 0;
    public string path = "";
    public int next_move = 0;
    public StimulusSender theSender = null;
    public StimulusSender theListener = null;
    ColorBlock cb;

    // NorthA = Up, SouthA = Left, Asia = Right, Africa = Backward, Oceania = Down, Europe = Forward
    public Button Up; //image to toggle.
    public Button Down; //image to toggle 
    public Button Left; //image to toggle
    public Button Right; //image to toggle
    public Button Left_Turn; //image to toggle  
    public Button Right_Turn; //image to toggle 
    public Button Forward; //image to toggle 

    public int num_of_blink_arrow = 2;
    public float current_time = 0.0f;

    public float interval = 0.1f;
    public float startDelay = 0.0f;
    public float timebetweenarrows = 0.1f;

    public int blinkcnt = 0;
    public int BlinkCount = blink_per_button * num_of_button;

    bool isBlinking = false;
    bool Button0 = true, Button1 = true, Button2 = true, Button3 = true, Button4 = true, Button5 = true, Button6 = true;
    public int up = 0, down = 0, left = 0, right = 0, left_turn = 0, right_turn = 0, fward = 0;
    int count = 0;
    public byte buttonIndexNum = 0;
    int rndnum = 0;

    public byte finish = 9;
    public byte start = 8;

    public int[] ranArr = { 0, 1, 2, 3, 4, 5, 6 };
    public string outp = "";
    public string output = "";

    bool blinkstate = true;
    Button pubimg;

    RectTransform noArect;
    int firstload = 1;

    public static int moveCount = 0;
    public static int correctCount = 0;

    public int max_Move_performance = 20;
    public int max_Move_correction = 10;
    void OnEnable()
    {
        next_move = random = UnityEngine.Random.Range(1, 8);
        switch (next_move)
        {
            case 1:
                path = "Forward";
                break;
            case 2:
                path = "Up";
                break;
            case 3:
                path = "Right_Turn";
                break;
            case 4:
                path = "Right";
                break;
            case 5:
                path = "Down";
                break;
            case 6:
                path = "Left";
                break;
            case 7:
                path = "Left_turn";
                break;
            case 0:
                path = "Goal";
                break;
            default:
                break;
        }
        txt.text = "Look at " + path;

        if (firstload == 1)
        {
            theSender = new StimulusSender();
            theSender.open("localhost", 12140);
            theListener = new StimulusSender();
            theListener.open("localhost", 12240);
            firstload = 0;
        }

        cb.normalColor = Color.gray;
        cb.colorMultiplier = 1.5f;
        Forward.colors = cb;
        Up.colors = cb;
        Right_Turn.colors = cb;
        Right.colors = cb;
        Down.colors = cb;
        Left.colors = cb;
        Left_Turn.colors = cb;

        noArect = GetComponent<RectTransform>();
        Debug.Log("Enabled");
        current_time = 0.0f;
        blinkcnt = 0;
        Button0 = true; Button1 = true; Button2 = true; Button3 = true; Button4 = true; Button5 = true; Button6 = true;
        up = 0; down = 0; left = 0; right = 0; left_turn = 0; right_turn = 0; fward = 0;
        count = 0;
        buttonIndexNum = 0;
        rndnum = 0;
        outp = "";
        output = "";
        blinkstate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Menu");
        }
        current_time += Time.deltaTime;
        //Restart blinking
        if (current_time > 5.0f && blinkstate == true)
        {
            blinkstate = false;
            Button0 = true;
            Button1 = true;
            Button2 = true;
            Button3 = true;
            Button4 = true;
            Button5 = true;
            Button6 = true;
            blinkcnt = 0;
            BlinkButton();
        }

    }
    public void BlinkButton()
    {
        if (blinkcnt < BlinkCount)
        {
            rndnum = UnityEngine.Random.Range(0, 7);
            if (rndnum == 0 && Button0 == true)
            {
                Button0 = false;
                buttonIndexNum = 1;
                theSender.send(buttonIndexNum);
                pubimg = Forward;

                if (isBlinking)
                    return;
                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }
            }
            else if (rndnum == 0 && Button0 == false)
            {
                BlinkButton();
            }
            else if (rndnum == 1 && Button1 == true)
            {
                Button1 = false;
                buttonIndexNum = 2;
                theSender.send(buttonIndexNum);
                pubimg = Up;

                if (isBlinking)
                    return;

                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }
            }
            else if (rndnum == 1 && Button1 == false)
            {
                BlinkButton();
            }
            else if (rndnum == 2 && Button2 == true)
            {
                Button2 = false;
                buttonIndexNum = 3;
                theSender.send(buttonIndexNum);
                pubimg = Right_Turn;

                if (isBlinking)
                    return;

                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }
            }
            else if (rndnum == 2 && Button2 == false)
            {
                BlinkButton();
            }
            else if (rndnum == 3 && Button3 == true)
            {
                Button3 = false;
                buttonIndexNum = 4;
                theSender.send(buttonIndexNum);
                pubimg = Right;

                if (isBlinking)
                    return;

                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }

            }
            else if (rndnum == 3 && Button3 == false)
            {
                BlinkButton();
            }
            else if (rndnum == 4 && Button4 == true)
            {
                Button4 = false;
                buttonIndexNum = 5;
                theSender.send(buttonIndexNum);
                pubimg = Down;

                if (isBlinking)
                    return;

                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }

            }
            else if (rndnum == 4 && Button4 == false)
            {
                BlinkButton();
            }
            else if (rndnum == 5 && Button5 == true)
            {
                Button5 = false;
                buttonIndexNum = 6;
                theSender.send(buttonIndexNum);
                pubimg = Left;

                if (isBlinking)
                    return;

                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }

            }
            else if (rndnum == 5 && Button5 == false)
            {
                BlinkButton();
            }
            else if (rndnum == 6 && Button6 == true)
            {
                Button6 = false;
                buttonIndexNum = 7;
                theSender.send(buttonIndexNum);
                pubimg = Left_Turn;

                if (isBlinking)
                    return;

                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }

            }
            else if (rndnum == 6 && Button6 == false)
            {
                BlinkButton();
            }

            if (Button0 == false && Button1 == false && Button2 == false && Button3 == false && Button4 == false && Button5 == false && Button6 == false)
            {
                Button0 = true;
                Button1 = true;
                Button2 = true;
                Button3 = true;
                Button4 = true;
                Button5 = true;
                Button6 = true;
            }
        }
        else
        {
            System.Threading.Thread.Sleep(1000);
            theSender.send(finish);
            Debug.Log("Receiving output");
            output = theListener.receive();
            Debug.Log("Received output");
            theSender.send(start);
            //theSender.close();
            //theListener.close();
            System.Threading.Thread.Sleep(1000);

            moveCount++;
            FileStream f = new FileStream(Application.dataPath + "/StreamingAssets/" + InputName.patient_id + ".txt", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);

            int result = 0;
            switch (output)
            {
                case "1":
                    result = 1;
                    break;
                case "2":
                    result = 2;
                    break;
                case "3":
                    result = 3;
                    break;
                case "4":
                    result = 4;
                    break;
                case "5":
                    result = 5;
                    break;
                case "6":
                    result = 6;
                    break;
                case "7":
                    result = 7;
                    break;
                default:
                    result = 0;
                    break;
            }
            writer.WriteLine("Order: " + next_move + " / Result: " + output);
            if (next_move == int.Parse(output))
            {
                correctCount++;

            }
            else
            {
                result = 0;
                txt.text = "Wrong Answer!";
            }
            writer.Close();
            Move_with_ControlPanel_Seven_AR.outpNum = result;
            Move_with_ControlPanel_Seven.ControlPanel_On_Off = 0;
            ControlPanel.SetActive(false);
        }
    }
    public void ToggleState()
    {
        if (cb.normalColor == Color.gray)
        {
            int color_choice = UnityEngine.Random.Range(0, 4);
            switch (color_choice)
            {
                case 0:
                    cb.normalColor = Color.yellow;
                    break;
                case 1:
                    cb.normalColor = Color.blue;
                    break;
                case 2:
                    cb.normalColor = Color.green;
                    break;
                case 3:
                    cb.normalColor = Color.white;
                    break;
            }
            //cb.normalColor = Color.yellow;
            pubimg.colors = cb;
        }
        else
        {
            cb.normalColor = Color.gray;
            pubimg.colors = cb;
        }
        count++;
        if (count == num_of_blink_arrow)
        {
            CancelInvoke();
            blinkcnt++;

            if (rndnum == 0) fward++;
            else if (rndnum == 1) up++;
            else if (rndnum == 2) right_turn++;
            else if (rndnum == 3) right++;
            else if (rndnum == 4) down++;
            else if (rndnum == 5) left++;
            else left_turn++;

            count = 0;
            isBlinking = false;
            Invoke("BlinkButton", timebetweenarrows);
        }
    }
    /*public int Navigate()
    {
        int result = 0;
        Vector3 Goal = Move_with_ControlPanel_Seven.Goal.transform.position;
        print("Goal: " + Goal);
        print("Current position: " + transform.position);
        int DifX = (int)(Goal.x - transform.position.x);
        int DifY = (int)(Goal.y - transform.position.y);
        int DifZ = (int)(Goal.z - transform.position.z);
        print("Difs: x: " + DifX + " y: " + DifY + " z: " + DifZ);
        DifX = DifX / 25;
        DifY = DifY / 25;
        DifZ = DifZ / 25;
        int moveCountX = 0;
        int moveCountY = 0;
        int moveCountZ = 0;
        switch (Move_with_ControlPanel_Seven.Dir)
        {
            case 0:
                moveCountX = DifX;
                moveCountY = DifY;
                moveCountZ = DifZ;
                break;
            case 1:
                moveCountX = -DifZ;
                moveCountY = DifY;
                moveCountZ = DifX;
                break;
            case 2:
                moveCountX = -DifX;
                moveCountY = DifY;
                moveCountZ = -DifZ;
                break;
            case 3:
                moveCountX = DifZ;
                moveCountY = DifY;
                moveCountZ = -DifX;
                break;
        }
        print("path: x-" + moveCountX + " y-" + moveCountY + " z-" + moveCountZ);

        if (moveCountZ >= 1)
        {
            result = 1;
        }
        else if (moveCountZ <= -1)
        {
            result = 3;
        }
        else if (moveCountX >= 1)
        {
            if (moveCountX > 1)
            {
                result = 3;
            }
            else
            {
                result = 4;
            }
        }
        else if (moveCountX <= -1)
        {
            if (moveCountX < -1)
            {
                result = 7;
            }
            else
            {
                result = 6;
            }
        }
        else if (moveCountY >= 1)
        {
            result = 2;
        }
        else if (moveCountY <= -1)
        {
            result = 5;
        }
        else
        {
            result = 0;
        }
        return result;
    }*/
}
