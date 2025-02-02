#include <fstream>
#include <iostream>
#include "day12.hpp"

using namespace AoC2015::Puzzles;

using std::cout, std::endl;
using std::string;
using std::vector;

const string Day12::FILENAME = "puzzles/12/input.txt";

void Day12::run()
{
    string input = read_input();

    JArray *jItems = JArray::parse(input);
    JArray *withoutRedValues = exclude_objects_with_red_values(jItems);

    int part1 = sum_all_numbers(jItems);
    int part2 = sum_all_numbers(withoutRedValues);

    cout << "** Day 12 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

int Day12::sum_all_numbers(JArray *jArr)
{
    int sum = 0;

    for (JItem *jItem : jArr->items)
    {
        if (jItem->type == "array")
            sum += sum_all_numbers((JArray *)jItem);
        else if (jItem->type == "object")
            sum += sum_all_numbers((JObject *)jItem);
        else if (jItem->type == "int")
            sum += ((JInt *)jItem)->value;
    }

    return sum;
}

int Day12::sum_all_numbers(JObject *jObj)
{
    int sum = 0;

    for (const auto &[_, jItem] : jObj->properties)
    {
        // JItem *jItem = property.second;

        if (jItem->type == "array")
            sum += sum_all_numbers((JArray *)jItem);
        else if (jItem->type == "object")
            sum += sum_all_numbers((JObject *)jItem);
        else if (jItem->type == "int")
            sum += ((JInt *)jItem)->value;
    }

    return sum;
}

JArray *Day12::exclude_objects_with_red_values(JArray *inputArr)
{
    JArray *outputArr = new JArray();

    for (JItem *jItem : inputArr->items)
    {
        if (jItem->type == "object")
            outputArr->items.push_back(exclude_objects_with_red_values((JObject *)jItem));
        else if (jItem->type == "array")
            outputArr->items.push_back(exclude_objects_with_red_values((JArray *)jItem));
        else
            outputArr->items.push_back(jItem);
    }

    return outputArr;
}

JObject *Day12::exclude_objects_with_red_values(JObject *inputObj)
{
    JObject *outputObj = new JObject();
    bool hasRedValue = false;

    for (const auto &[name, jItem] : inputObj->properties)
    {
        // string name = property.first;
        // JItem *jItem = property.second;
        std::pair<string, JItem *> newProperty;

        if (jItem->type == "object")
            newProperty = {name, exclude_objects_with_red_values((JObject *)jItem)};
        else if (jItem->type == "array")
            newProperty = {name, exclude_objects_with_red_values((JArray *)jItem)};
        else
        {
            newProperty = {name, jItem};

            if (jItem->type == "string")
                hasRedValue |= ((JString *)jItem)->value == "red";
        }

        outputObj->properties.push_back(newProperty);
    }

    return hasRedValue ? new JObject() : outputObj;
}

string Day12::read_input()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    string input, line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        input += line;
    }

    return input;
}

JItem *JItem::parse(const string &s)
{
    switch (s[0])
    {
    case '{':
        return JObject::parse(s);
    case '[':
        return JArray::parse(s);
    case '"':
        return JString::parse(s);
    default:
        return JInt::parse(s);
    }
}

JArray::JArray()
{
    type = "array";
}

JArray *JArray::parse(const string &s)
{
    vector<string> tokens = JArray::tokenize_string(s);
    JArray *jArr = new JArray();

    for (const string &token : tokens)
    {
        JItem *jItem = JItem::parse(token);

        jArr->items.push_back(jItem);
    }

    return jArr;
}

JObject *JObject::parse(const string &s)
{
    vector<string> tokens = JObject::tokenize_string(s);
    JObject *jObj = new JObject();

    for (const string &token : tokens)
    {
        size_t sepIdx = token.find(':');

        string name = token.substr(0, sepIdx);
        string value = token.substr(sepIdx + 1);

        JItem *jItem = JItem::parse(value);

        jObj->properties.push_back({name, jItem});
    }

    return jObj;
}

JObject::JObject()
{
    type = "object";
}

JInt::JInt(int val)
{
    value = val;
    type = "int";
}

JInt *JInt::parse(const string &s)
{
    return new JInt(stoi(s));
}

JString::JString(const string &val)
{
    value = val;
    type = "string";
}

JString *JString::parse(const string &s)
{
    return new JString(s.substr(1, s.size() - 2));
}

vector<string> JArray::tokenize_string(const string &s)
{
    vector<string> tokens;
    string token;

    for (size_t i = 1; i < s.size() - 1; i++)
    {
        if (s[i] == ',')
            continue;

        token.clear();

        if (s[i] == '[' || s[i] == '{')
        {
            char openChar = s[i], closeChar = openChar == '{' ? '}' : ']';
            size_t openCharCount = 0;

            do
            {
                if (s[i] == openChar)
                    openCharCount++;
                else if (s[i] == closeChar)
                    openCharCount--;

                token += s[i++];
            } while (openCharCount != 0);
        }
        else
        {
            while (i < s.size() - 1 && s[i] != ',')
                token += s[i++];
        }

        tokens.push_back(token);
    }

    return tokens;
}

vector<string> JObject::tokenize_string(const string &s)
{
    vector<string> tokens;
    string token;

    for (size_t i = 1; i < s.size() - 1; i++)
    {
        if (s[i] == ',')
            continue;

        token.clear();

        while (s[i] != ':')
            token += s[i++];

        token += s[i++];

        if (s[i] == '[' || s[i] == '{')
        {
            char openChar = s[i], closeChar = openChar == '{' ? '}' : ']';
            size_t openCharCount = 0;

            do
            {
                if (s[i] == openChar)
                    openCharCount++;
                else if (s[i] == closeChar)
                    openCharCount--;

                token += s[i++];
            } while (openCharCount != 0);
        }
        else
        {
            while (i < s.size() - 1 && s[i] != ',')
                token += s[i++];
        }

        tokens.push_back(token);
    }

    return tokens;
}
