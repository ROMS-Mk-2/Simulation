mergeInto(LibraryManager.library, {
  SendData: function (data) {
    window.dispatchReactUnityEvent("SendData", data);
  },
});