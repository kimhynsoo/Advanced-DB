const express = require('express');
const app = express();
app.use(express.json());
app.use(require('./routes/student'));

app.use(function(err,req,res){
    console.error(err.stack);
    res.status(500).send('An error occurred!');
});

const PORT = 3000;
const dbo = require('./db/connection');
dbo.connectToserver(function(err){
    if(err){
        console.error(err);
        process.exit(1);
    } 
}).then(()=>{
    app.listen(PORT,()=>{
        console.log('Servier is running on port: '+PORT);
    });
});