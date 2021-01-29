const inputEl = document.getElementById("Content");
const countEl = document.getElementById("char-count");

if (inputEl) {
    const maxLength = inputEl.getAttribute("maxlength");

    countEl.innerHTML = maxLength - inputEl.value.length;

    inputEl.addEventListener("keyup", function () {
        countEl.innerHTML = maxLength - inputEl.value.length;
    });
}