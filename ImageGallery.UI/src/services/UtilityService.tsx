export const resetSearchPhrase = () => {
  localStorage.setItem("search", "");

  window.location.reload();
};
