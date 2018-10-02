# Bionic Electron Template

Electron template for Bionic Projects

## Deployment Notes:

There are a few things to take into consideration when creating your Blazor WASM project for Electron deployment
1. Root page must include a second page route: @page("/index.html")
1. electron/index.html must have a base haref of "./": &#60;base href="./" /&#62;
