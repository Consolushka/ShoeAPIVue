const ROOT = "http://localhost:5000/";

export const variables = {
  API: {
    BRANDS: `${ROOT}api/brands/`,
    SHOES:`${ROOT}api/shoes/`
  },
  PHOTO_URL: `${ROOT}photos/`,
  CloseModal(type, model) {
    document.querySelector(`#${type}${model}Modal`).classList.remove("show");
    document.querySelector(`#${type}${model}Modal`).setAttribute("style", "display: none");
    document.querySelector(`#${type}${model}Modal`).removeAttribute("aria-modal");
    document.querySelector(".modal-backdrop").remove();
    document.querySelector("body").classList.remove("modal-open");
    document.querySelector("body").removeAttribute("style");
  }
}
