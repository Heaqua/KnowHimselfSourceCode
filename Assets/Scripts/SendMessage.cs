using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JSONInformation
{
    public string username;
    public string text;
}

public class SendMessage : MonoBehaviour {
    public Font customFont;
    public Button SubmitButton;
    public Canvas canvasObject;
    public InputField TextInput;
    public int indexcounter = 0;
    private Text text;

    float paddingX = -2F;
    float paddingY = 410F;
    float padding = 600F;
    float height = 22;
    ushort maxMessagesToDisplay = 11;

    string channel = "chatchannel";

    // Create a chat message queue so we can interate through all the messages
    Queue<GameObject> chatMessageQueue = new Queue<GameObject>();
   
    void Start()
    {
        // Add Listener to Submit button to send messages
        Button btn = SubmitButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        
        // Add the starting text
        JSONInformation startingText = new JSONInformation();
        startingText.username = "- ";
        startingText.text = "Leave me alone.";
        CreateChat(startingText);

        
        
    }

    // Function used to create new chat objects based of the data received from PubNub
    void CreateChat(JSONInformation payLoad){

        // Create a string with the username and text
        string currentObject = string.Concat(payLoad.username, payLoad.text);

        // Create a new gameobject that will display text of the data sent via PubNub
        GameObject chatMessage = new GameObject(currentObject);
        chatMessage.transform.SetParent(canvasObject.GetComponent<Canvas>().transform);
        chatMessage.transform.localPosition = Vector3.zero;
        chatMessage.transform.position = new Vector3(canvasObject.transform.position.x - paddingX, canvasObject.transform.position.y - paddingY + padding - (indexcounter * height), 1F);        
        chatMessage.AddComponent<Text>().text = currentObject;

        // Assign text to the gameobject. Add visual properties to text
        var chatText = chatMessage.GetComponent<Text>();
        chatText.font = customFont;
        chatText.color = UnityEngine.Color.white;
        chatText.fontSize = 18;
        chatText.verticalOverflow = VerticalWrapMode.Overflow;

        // Assign a RectTransform to gameobject to maniuplate positioning of chat.
        RectTransform rectTransform;
        rectTransform = chatText.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(600, height);
        
        // Assign the gameobject to the queue of chatmessages
        chatMessageQueue.Enqueue(chatMessage);
                       
        // Keep track of how many objects we have displayed on the screen
        indexcounter++;
        SyncChat();
    }

    void SyncChat() {
        // If more maxMessagesToDisplay objects are on the screen, we need to start removing them
        if (indexcounter > maxMessagesToDisplay)
        {
            // Delete the first gameobject in the queue
            GameObject deleteChat = chatMessageQueue.Dequeue();
            Destroy(deleteChat);

            // Move all existing text gameobjects up the Y axis 
            int c = 0;
            foreach (GameObject moveChat in chatMessageQueue)
            {
                RectTransform moveText = moveChat.GetComponent<RectTransform>();
                moveText.position = new Vector3(canvasObject.transform.position.x - paddingX, canvasObject.transform.position.y - paddingY + padding - (c * height), 1F);
                c++;
            }
            indexcounter--;
        }


    }

	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick()
    {
        // When the user clicks the Submit button,
        // create a JSON object from input field input
        JSONInformation publishMessage = new JSONInformation();
        publishMessage.username =  "> ";
        publishMessage.text = TextInput.text;

        CreateChat(publishMessage);

        Reply(TextInput.text);
        
        TextInput.text = "";
    }

    void Reply( string input)
    {
        // test regret 
        input = input.ToUpper();
        bool regret = input.Contains("SORRY");

        // give response
        JSONInformation response = new JSONInformation();
        response.username = "- ";
        response.text = regret
            ? "Haha poor guy. I like your embarrassing face. " +
              "I will allow you to be fooled again cuz I just have mercy."
            : "I said leave me alone.";
        CreateChat(response);
    }
}
