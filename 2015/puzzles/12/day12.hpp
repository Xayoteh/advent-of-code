#ifndef DAY12_H
#define DAY12_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class JArray;
    class JObject;

    class Day12
    {
    public:
        Day12() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static int sum_all_numbers(JArray *);
        static int sum_all_numbers(JObject *);

        static std::string read_input();

        static JArray *exclude_objects_with_red_values(JArray *);
        static JObject *exclude_objects_with_red_values(JObject *);
    };

    class JItem
    {
    public:
        std::string type;

        static JItem *parse(const std::string &);
    };

    class JArray : public JItem
    {
    public:
        std::vector<JItem *> items;

        JArray();

        static std::vector<std::string> tokenize_string(const std::string &);

        static JArray *parse(const std::string &);
    };

    class JInt : public JItem
    {
    public:
        int value;

        JInt(int val);

        static JInt *parse(const std::string &);
    };

    class JObject : public JItem
    {
    public:
        std::vector<std::pair<std::string, JItem *>> properties;

        JObject();

        static std::vector<std::string> tokenize_string(const std::string &);

        static JObject *parse(const std::string &);
    };

    class JString : public JItem
    {
    public:
        std::string value;

        JString(const std::string &val);

        static JString *parse(const std::string &);
    };
}

#endif