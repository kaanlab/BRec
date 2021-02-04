// New instance
const recorder = new MicRecorder({
    bitRate: 128
});

window.MyRecorder = {

    // Start recording. Browser will request permission to use your microphone.
    start: function () {
        recorder.start().then(() => {
            // something else
        }).catch((e) => {
            console.error(e);
        });
    },

    // Once you are done singing your best song, stop and get the mp3.
    stop: function (param) {
        recorder.stop()
            .getMp3().then(([buffer, blob]) => {
                // do what ever you want with buffer and blob
                // Example: Create a mp3 file and play
                const filename = param + '.mp3';
                const file = new File(buffer, filename, {
                    type: blob.type,
                    lastModified: Date.now()
                });

                const a = document.createElement("a");
                document.body.appendChild(a);
                a.style = "display: none";
                const li = document.createElement('span');
                const player = new Audio(URL.createObjectURL(file));
                player.controls = true;
                li.appendChild(player);
                document.querySelector('#playlist').appendChild(li);
                a.href = URL.createObjectURL(file);
                a.download = file.name;
                a.click();
                const fd = new FormData();
                fd.append("file", file, file.name);
                const xhr = new XMLHttpRequest();
                xhr.addEventListener("load", transferComplete);
                xhr.addEventListener("error", transferFailed)
                xhr.addEventListener("abort", transferFailed)
                xhr.open("POST", "api/Upload/Save/", true);
                xhr.send(fd);

            }).catch((e) => {
                alert('We could not retrieve your message');
                console.log(e);
            });
    }
};

function transferComplete(evt) {
    console.log("The transfer is complete.");
    //GLOBAL.DotNetReference.invokeMethodAsync('Recognize', filename);
}
function transferFailed(evt) {
    console.log("An error occurred while transferring the file.");
    console.log(evt.responseText);
    console.log(evt.status);
}
