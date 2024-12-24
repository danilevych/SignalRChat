"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");

    var senderSpan = document.createElement("span");
    senderSpan.classList.add("sender");
    senderSpan.textContent = user;

    var saysSpan = document.createElement("span");
    saysSpan.classList.add("says");
    saysSpan.textContent = " says ";

    var messageSpan = document.createElement("span");
    messageSpan.classList.add("message");
    messageSpan.textContent = message;

    li.appendChild(senderSpan);
    li.appendChild(saysSpan);
    li.appendChild(messageSpan);

    document.getElementById("messagesList").appendChild(li);

    document.getElementById("messageInput").value = "";
});


connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
