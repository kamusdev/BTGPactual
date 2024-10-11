#!/bin/bash

TABLE_NAME="FondosBTG"

# Check if the table exists
if aws dynamodb list-tables --endpoint-url http://dynamodb:8000 | grep -q "$TABLE_NAME"; then
    echo "Table $TABLE_NAME already exists. Skipping creation."
else
    # Create a table using AWS CLI
    aws dynamodb create-table \
        --table-name $TABLE_NAME \
        --attribute-definitions \
            AttributeName=pk,AttributeType=S \
            AttributeName=sk,AttributeType=S \
        --key-schema \
            AttributeName=pk,KeyType=HASH \
            AttributeName=sk,KeyType=RANGE \
        --provisioned-throughput \
            ReadCapacityUnits=5,WriteCapacityUnits=5 \
        --endpoint-url http://dynamodb:8000

    echo "Table $TABLE_NAME created."
fi