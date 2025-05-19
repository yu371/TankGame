mergeInto(LibraryManager.library, {
  StartWebSocket: function (urlPtr) {
    const url = UTF8ToString(urlPtr);
    window.socket = new WebSocket(url);

    window.socket.onopen = () => {
      console.log("✅ WebSocket connected");
    };

    window.socket.onmessage = (e) => {
      console.log("📩 Message from server:", e.data);

      // Unityの GameObject にメッセージを渡す
      SendMessage("ReceiverObject", "OnMessageFromServer", e.data);
    };
  },

  SendWebSocketMessage: function (msgPtr) {
    const msg = UTF8ToString(msgPtr);
    if (window.socket && window.socket.readyState === WebSocket.OPEN) {
      window.socket.send(msg);
    }
  },
});
