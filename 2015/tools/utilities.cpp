#include <sstream>
#include "utilities.hpp"

using namespace AoC2015::Tools;

using std::string;
using std::vector;

vector<string> Utilities::split(const string &s, char delim)
{
    vector<string> strings;
    std::istringstream iss(s);
    string line;

    while (!iss.eof())
    {
        getline(iss, line, delim);
        strings.push_back(line);
    }

    return strings;
}

vector<string> Utilities::split(const string &s, const string &delim)
{
    vector<string> strings;
    size_t delimIdx, startIdx = 0;
    string line;

    while ((delimIdx = s.find(delim, startIdx)) != string::npos)
    {
        line = s.substr(startIdx, delimIdx - startIdx);
        strings.push_back(line);
        startIdx = delimIdx + delim.size();
    }

    strings.push_back(s.substr(startIdx));

    return strings;
}
