const count = document.getElementById("count");
const button = document.getElementById("increment");

button.onclick = () => {
  const value = Number(count.textContent);
  count.textContent = value + 1;
};
