module.exports = {
  apps : [{
    name: "ProdFront",
    script: "./preptm/FrontEnd/Prod/client/proxy-server.js"
   
  },
{
    name: "StageFront",
    script: "./preptm/FrontEnd/Stage/client/proxy-server.js"
   
  }]
}