const express = require('express');
const studentRoutes = express.Router();
const dbo = require('../db/connection');

studentRoutes.route('/students').post(function(req, res){
    const dbConnect = dbo.getDb();

    const newStudentDocument = {
        _id: req.body.id,
        first_name: req.body.first_name,
        last_name: req.body.last_name,
        age: req.body.age

    };

    dbConnect
    .collection('students')
    .insertOne(newStudentDocument, function(err, result){
        if(err){
            res.status(400).send('Error inserting new student!');
        } else{
            console.log('Added a new student with id '+ result.insertedId);
            res.status(200).send();
        }
    });
});

studentRoutes.route('/students').get(function(req, res){
    const dbConnect = dbo.getDb();

    dbConnect
    .collection('students')
    .find({})
    .limit(50)
    .toArray(function(err, result){
        if(err){
            res.status(400).send('Error fetching students!');
        } else{
            res.json(result);
        }
    }); 
});

module.exports = studentRoutes;