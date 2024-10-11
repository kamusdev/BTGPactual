#!/bin/bash

# Seed the table with initial data
aws dynamodb put-item \
    --table-name FondosBTG \
    --item '{
        "pk": {"S": "F"},
        "sk": {"S": "1"},
        "name": {"S": "FPV_BTG_PACTUAL_RECAUDADORA"},
        "minValue": {"N": "75000"},
        "category": {"S": "FPV"}
    }' \
    --endpoint-url http://dynamodb:8000

aws dynamodb put-item \
    --table-name FondosBTG \
    --item '{
        "pk": {"S": "F"},
        "sk": {"S": "2"},
        "name": {"S": "FPV_BTG_PACTUAL_ECOPETROL"},
        "minValue": {"N": "125000"},
        "category": {"S": "FPV"}
    }' \
    --endpoint-url http://dynamodb:8000

aws dynamodb put-item \
    --table-name FondosBTG \
    --item '{
        "pk": {"S": "F"},
        "sk": {"S": "3"},
        "name": {"S": "DEUDAPRIVADA"},
        "minValue": {"N": "50000"},
        "category": {"S": "FIC"}
    }' \
    --endpoint-url http://dynamodb:8000

aws dynamodb put-item \
    --table-name FondosBTG \
    --item '{
        "pk": {"S": "F"},
        "sk": {"S": "4"},
        "name": {"S": "FDO-ACCIONES"},
        "minValue": {"N": "250000"},
        "category": {"S": "FIC"}
    }' \
    --endpoint-url http://dynamodb:8000

aws dynamodb put-item \
    --table-name FondosBTG \
    --item '{
        "pk": {"S": "F"},
        "sk": {"S": "5"},
        "name": {"S": "FPV_BTG_PACTUAL_DINAMICA"},
        "minValue": {"N": "100000"},
        "category": {"S": "FPV"}
    }' \
    --endpoint-url http://dynamodb:8000

aws dynamodb put-item \
    --table-name FondosBTG \
    --item '{
        "pk": {"S": "C"},
        "sk": {"S": "1"},
        "name": {"S": "Luis Fernando Arbelaez Rojas"},
        "email": {"S": "anewdev@gmail.com"},
        "balance": {"N": "390000"}
    }' \
    --endpoint-url http://dynamodb:8000

echo "Records added."

