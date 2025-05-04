const req = require('express/lib/request');
const {MongoClient} = require('mongodb');
const connectionString = 'mongodb://127.0.0.1:27017';

const client = new MongoClient(connectionString,{
    useNewUrlParser: true,
    useUnifiedTopology: true,
});

let dbConnection;

module.exports = {
    connectToserver : async function(){
        const db = await client.connect();
        dbConnection = db.db('school');
        console.log("Successfully connected to MongoDB.");
    },

    getDb : function(){
        return dbConnection;
    }
};

